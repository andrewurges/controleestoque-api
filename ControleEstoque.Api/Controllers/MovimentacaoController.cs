using ControleEstoque.Api.Helpers;
using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Api.Services;
using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private IControleEstoqueService<Pedido> _pedidoService;
        private IControleEstoqueService<Cliente> _clienteService;
        private readonly ILogger<MovimentacaoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="movimentacaoService"></param>
        /// <param name="estoqueService"></param>
        /// <param name="pedidoService"></param>
        /// <param name="produtoService"></param>
        /// <param name="clienteService"></param>
        /// <param name="logger"></param>
        public MovimentacaoController(MovimentacaoService movimentacaoService, 
            EstoqueService estoqueService,
            PedidoService pedidoService,
            ProdutoService produtoService,
            ClienteService clienteService,
            ILogger<MovimentacaoController> logger)
        {
            _movimentacaoService = movimentacaoService;
            _estoqueService = estoqueService;
            _pedidoService = pedidoService;
            _produtoService = produtoService;
            _clienteService = clienteService;
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
                    .OrderByDescending(x => x.Data)
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
        ///     Realiza o registro de uma receita no fluxo de caixa.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados da receita</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("registrar-receita")]
        [Produces("application/json")]
        public IActionResult RegistrarReceita([FromBody] RegistrarReceitaRequest requestBody)
        {
            try
            {
                var novaMovimentacao = (MovimentacaoDTO)_movimentacaoService.Create(new Movimentacao()
                {
                    Tipo = ETipoMovimentacao.Receita,
                    Data = DateHelper.GetCurrentDateTime(),
                    IdPedido = requestBody.IdPedido
                });

                var lst = new List<MovimentacaoDTO>() { novaMovimentacao };

                return Ok(GetMovimentacaoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/registrar-receita - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza o registro de uma despesa fluxo de caixa.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados da despesa</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("registrar-despesa")]
        [Produces("application/json")]
        public IActionResult RegistrarDespesa([FromBody] RegistrarDespesaRequest requestBody)
        {
            try
            {
                var novaMovimentacao = (MovimentacaoDTO)_movimentacaoService.Create(new Movimentacao()
                {
                    Tipo = ETipoMovimentacao.Despesa,
                    Data = DateHelper.GetCurrentDateTime(),
                    ItensEstoque = requestBody.ItensEstoque
                });

                var lst = new List<MovimentacaoDTO>() { novaMovimentacao };

                return Ok(GetMovimentacaoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"movimentacao/registrar-despesa - {e.Message}");
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
                    Pedido = !string.IsNullOrEmpty(e.IdPedido) ? GetPedidoEnumerable(_pedidoService.Get(ObjectId.Parse(e.IdPedido))) : null,
                    ItensEstoque = e.ItensEstoque != null && e.ItensEstoque.Count > 0 ?
                        from t in e.ItensEstoque.AsQueryable()
                        select new
                        {
                            Estoque =  t.IdEstoque != "" ? (EstoqueDTO)_estoqueService.Get(ObjectId.Parse(t.IdEstoque)) : null,
                            t.Valor,
                            t.Quantidade
                        } : null,
                    e.Valor
                };
        }

        private object GetPedidoEnumerable(PedidoDTO pedido) => new
        {
            pedido.Id,
            Cliente = (ClienteDTO)_clienteService.Get(ObjectId.Parse(pedido.IdCliente)),
            ListaProduto =
                        from t in pedido.ListaProduto.AsQueryable()
                        select new
                        {
                            Produto = (ProdutoDTO)_produtoService.Get(ObjectId.Parse(t.IdProduto)),
                            t.Quantidade,
                            t.PrecoUnidade
                        },
            pedido.Historico,
            pedido.DataCriacao,
            pedido.DataAtualizacao,
            pedido.SituacaoPedido,
            pedido.SituacaoPagamento,
            pedido.Desconto,
            pedido.Total
        };
    }
}
