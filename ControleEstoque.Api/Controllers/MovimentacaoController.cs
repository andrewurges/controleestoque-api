using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Api.Services;
using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ControleEstoque.Api.Controllers
{
    /// <summary>
    ///     Controller para gerenciar as movimentações do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private IControleEstoqueService<Movimentacao> _movimentacaoService;
        private readonly ILogger<MovimentacaoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="movimentacaoService"></param>
        /// <param name="logger"></param>
        public MovimentacaoController(MovimentacaoService movimentacaoService, ILogger<MovimentacaoController> logger)
        {
            _movimentacaoService = movimentacaoService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todas as movimentações.
        /// </summary>
        /// <returns>Lista de movimentação</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public RequestResponse ListarTodaMovimentacao()
        {
            try
            {
                List<Movimentacao> lst = _movimentacaoService.GetAll();

                var query =
                    (from e in lst.AsQueryable<Movimentacao>()
                     select new MovimentacaoDTO(e)).ToList();

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = query
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/listar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a criação de uma nova movimentação.
        /// </summary>
        /// <param name="request">Objeto com os dados da movimentação</param>
        [HttpPost("criar")]
        [Produces("application/json")]
        public RequestResponse CriarMovimentacao([FromBody] Movimentacao request)
        {
            try
            {
                if (!_movimentacaoService.Create(request))
                    throw new Exception("Não foi possível incluir a movimentação.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Movimentação incluída com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/criar - {e.Message}");
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
