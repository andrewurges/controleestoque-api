using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Api.Services;
using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
        private IControleEstoqueService<Estoque> _estoqueService;
        private IControleEstoqueService<Produto> _produtoService;
        private readonly ILogger<MovimentacaoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="movimentacaoService"></param>
        /// <param name="estoqueService"></param>
        /// <param name="produtoService"></param>
        /// <param name="logger"></param>
        public MovimentacaoController(MovimentacaoService movimentacaoService, 
            EstoqueService estoqueService,
            ProdutoService produtoService,
            ILogger<MovimentacaoController> logger)
        {
            _movimentacaoService = movimentacaoService;
            _estoqueService = estoqueService;
            _produtoService = produtoService;
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
                List<MovimentacaoDTO> lst = _movimentacaoService.GetAll().Select(x => (MovimentacaoDTO)x)
                    .OrderByDescending(x => DateTime.Parse(x.Data))
                    .ToList();

                return Ok(GetMovimentacaoEnumerable(lst));
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
                var novaMovimentacao = (MovimentacaoDTO)_movimentacaoService.Create(new Movimentacao()
                {
                    Tipo = requestBody.Tipo,
                    Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                    Itens = requestBody.Itens
                });

                var lst = new List<MovimentacaoDTO>() { novaMovimentacao };

                return Ok(GetMovimentacaoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        private IEnumerable<object> GetMovimentacaoEnumerable(List<MovimentacaoDTO> lst)
        {
            return
                from e in lst.AsQueryable()
                select new
                {
                    e.Id,
                    e.Tipo,
                    e.Data,
                    Itens = from t in e.Itens.AsQueryable()
                                   select new
                                   {
                                       Estoque =  t.IdEstoque != "" ? (EstoqueDTO)_estoqueService.Get(ObjectId.Parse(t.IdEstoque)) : null,
                                       Produto = t.IdProduto != "" ? (ProdutoDTO)_produtoService.Get(ObjectId.Parse(t.IdProduto)) : null,
                                       t.Valor,
                                       t.Quantidade
                                   }
                };
        }
    }
}
