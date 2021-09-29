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
        /// <param name="requestBody">Objeto com os dados do usuário</param>
        /// <returns>Token de autenticação do usuário</returns>
        [HttpPost("autenticar")]
        [Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarRequest requestBody)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(requestBody.Email, requestBody.Password, false, false);

                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == requestBody.Email);

                    var token = AuthenticationHelper.GenerateJwtToken(requestBody.Email, appUser, _configuration);

                    return Ok(new AutenticarResponse(token, appUser.UserName, appUser.Name + " " + appUser.LastName));
                }

                throw new Exception("Bad Credentials");
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/autenticar - " + e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza o cadastro de um novo e-mail na API.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados do novo usuário</param>
        /// <returns>Novo usuário com o token de autenticação</returns>
        [HttpPost("registrar")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Registrar(RegistrarUsuarioRequest requestBody)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(requestBody.Email);
                if (user != null)
                {
                    throw new Exception($"E-mail {requestBody.Email} já cadastrado.");
                }

                var newUser = new ApplicationUser
                {
                    Name = requestBody.Name,
                    LastName = requestBody.LastName,
                    UserName = requestBody.Email,
                    Email = requestBody.Email,
                };

                var result = await _userManager.CreateAsync(newUser, requestBody.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, false);
                    var token = AuthenticationHelper.GenerateJwtToken(requestBody.Email, newUser, _configuration);

                    return Ok(new AutenticarResponse(token, newUser.Email, newUser.UserName));
                }

                throw new Exception(string.Join(",", result.Errors?.Select(error => error.Description)));
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/registrar - " + e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização de um usuário conforme o E-mail informado.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser atualizado</param>
        /// <param name="requestBody">Objeto com os dados atualizados do usuário</param>
        [HttpPost("atualizar/{email}")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Atualizar(string email, AtualizarUsuarioRequest requestBody)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception($"Usuário com e-mail {email} não foi encontrado.");
                }

                user.Name = requestBody.Name;
                user.LastName = requestBody.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(requestBody);
                }

                throw new Exception(string.Join(",", result.Errors?.Select(error => error.Description)));
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/atualizar - " + e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a alteração da senha de um usuário conforme o E-mail informado.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser atualizado</param>
        /// <param name="requestBody">Objeto com os dados da senha atual e nova</param>
        [HttpPost("trocar-senha/{email}")]
        [Produces("application/json")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> TrocarSenha(string email, AlterarSenhaRequest requestBody)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception($"Usuário com e-mail {email} não foi encontrado.");
                }

                var senhaValida = await _userManager.CheckPasswordAsync(user, requestBody.CurrentPassword);
                if (!senhaValida)
                {
                    throw new Exception("A senha atual informada está incorreta.");
                }

                if (!requestBody.NewPassword.Equals(requestBody.ConfirmNewPassword))
                {
                    throw new Exception("A nova senha e a confirmação da senha não correspondem.");
                }

                var result = await _userManager.ChangePasswordAsync(user, requestBody.CurrentPassword, requestBody.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(requestBody);
                }

                throw new Exception(string.Join(",", result.Errors?.Select(error => error.Description)));
            }
            catch (Exception e)
            {
                _logger.LogError("Usuario/trocar-senha - " + e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
