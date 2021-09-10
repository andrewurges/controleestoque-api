using ControleEstoque.Api.Helpers;
using ControleEstoque.Api.Model;
using ControleEstoque.Api.Model.Identity;
using ControleEstoque.Api.Model.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ControleEstoque.Api.Controllers
{
    /// <summary>
    ///     Controller para realizar a autenticação na API.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public UsuarioController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ILogger<UsuarioController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a autenticação na API.
        /// </summary>
        /// <param name="request">Objeto com os dados do usuário</param>
        /// <returns>Token de autenticação do usuário</returns>
        [HttpPost("autenticar")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<RequestResponse> Autenticar([FromBody] AutenticarRequest request)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == request.Email);

                    var token = AuthenticationHelper.GenerateJwtToken(request.Email, appUser, _configuration);

                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.OK,
                        Mensagem = string.Empty,
                        Resultado = new AutenticarResponse(token, appUser.UserName, appUser.Name + " " + appUser.LastName)
                    };
                }

                _logger.LogError("Usuario/Autenticar - Bad Credentials");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.Unauthorized,
                    Mensagem = "Bad Credentials",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/Autenticar - " + e.Message);
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza o cadastro de um novo e-mail na API.
        /// </summary>
        /// <param name="request">Objeto com os dados do novo usuário</param>
        /// <returns>Novo usuário com o token de autenticação</returns>
        [HttpPost("registrar")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<RequestResponse> Registrar(RegistrarUsuarioRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Mensagem = "E-mail " + request.Email + " já cadastrado.",
                        Resultado = null
                    };
                }

                var newUser = new ApplicationUser
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    UserName = request.Email,
                    Email = request.Email,
                };

                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, false);
                    var token = AuthenticationHelper.GenerateJwtToken(request.Email, newUser, _configuration);

                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.OK,
                        Mensagem = "Usuário cadastrado com sucesso!",
                        Resultado = new AutenticarResponse(token, newUser.Email, newUser.UserName)
                    };
                }

                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = string.Join(",", result.Errors?.Select(error => error.Description)),
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/Registrar - " + e.Message);
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a atualização de um usuário conforme o E-mail informado.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser atualizado</param>
        /// <param name="request">Objeto com os dados atualizados do usuário</param>
        [HttpPost("atualizar/{email}")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<RequestResponse> Atualizar(string email, AtualizarUsuarioRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Mensagem = "Usuário com e-mail " + email + " não foi encontrado.",
                        Resultado = null
                    };
                }

                user.Name = request.Name;
                user.LastName = request.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.OK,
                        Mensagem = "Usuário atualizado com sucesso!",
                        Resultado = null
                    };
                }

                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = string.Join(",", result.Errors?.Select(error => error.Description)),
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/Atualizar - " + e.Message);
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a alteração da senha de um usuário conforme o E-mail informado.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser atualizado</param>
        /// <param name="request">Objeto com os dados da senha atual e nova</param>
        [HttpPost("trocar-senha/{email}")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<RequestResponse> TrocarSenha(string email, AlterarSenhaRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Mensagem = "Usuário com e-mail " + email + " não foi encontrado.",
                        Resultado = null
                    };
                }

                var senhaValida = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                if (!senhaValida)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Mensagem = "A senha atual informada está incorreta.",
                        Resultado = null
                    };
                }

                if (!request.NewPassword.Equals(request.ConfirmNewPassword))
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.InternalServerError,
                        Mensagem = "A nova senha e a confirmação da senha não correspondem.",
                        Resultado = null
                    };
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (result.Succeeded)
                {
                    return new RequestResponse()
                    {
                        Status = HttpStatusCode.OK,
                        Mensagem = "Senha atualizada com sucesso!",
                        Resultado = null
                    };
                }

                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = string.Join(",", result.Errors?.Select(error => error.Description)),
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/TrocarSenha - " + e.Message);
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }
    }
}
