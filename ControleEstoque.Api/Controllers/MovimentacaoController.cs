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
        /// <returns>Lista de movimentações</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<MovimentacaoDTO> lst = _movimentacaoService.GetAll().Select(x => (MovimentacaoDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de uma nova movimentação.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados da movimentação</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] MovimentacaoRequest requestBody)
        {
            try
            {
                return Ok((MovimentacaoDTO)_movimentacaoService.Create(new Movimentacao() 
                { 
                    Tipo = requestBody.Tipo,
                    Data = DateTime.Now.ToString("dd/MM/yyyy"),
                    Itens = requestBody.Itens
                }));
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
