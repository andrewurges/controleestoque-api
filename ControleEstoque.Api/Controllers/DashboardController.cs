using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model.Response;
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
    ///     Controller para ter informações do Dashboard do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IControleEstoqueService<Produto> _produtoService;
        private IControleEstoqueService<Pedido> _pedidoService;
        private IControleEstoqueService<Movimentacao> _movimentacaoService;
        private IControleEstoqueService<Cliente> _clienteService;
        private readonly ILogger<EstoqueController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="produtoService"></param>
        /// <param name="pedidoService"></param>
        /// <param name="movimentacaoService"></param>
        /// <param name="clienteService"></param>
        /// <param name="logger"></param>
        public DashboardController(ProdutoService produtoService,
            PedidoService pedidoService,
            MovimentacaoService movimentacaoService,
            ClienteService clienteService,
            ILogger<EstoqueController> logger)
        {
            _produtoService = produtoService;
            _pedidoService = pedidoService;
            _movimentacaoService = movimentacaoService;
            _clienteService = clienteService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca do valor total de produtos em estoque.
        /// </summary>
        /// <returns>Valor total</returns>
        [HttpGet("total-estoque")]
        [Produces("application/json")]
        public IActionResult BuscarTotalEstoque()
        {
            try
            {
                var lst = _produtoService.GetAll(x => x.QuantidadeDisponivel > 0).Select(x => (ProdutoDTO)x).ToList();

                return Ok(new TotalEstoqueResponse()
                {
                    Quantidade = lst.Sum(x => x.QuantidadeDisponivel),
                    Total = lst.Sum(x => x.Preco * x.QuantidadeDisponivel),
                    Produtos = lst
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-estoque - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca do valor total em pedidos a receber.
        /// </summary>
        /// <returns>Valor total</returns>
        [HttpGet("total-receber")]
        [Produces("application/json")]
        public IActionResult BuscarTotalReceber()
        {
            try
            {
                return Ok(new TotalReceberResponse()
                {
                    Total = _pedidoService.GetAll(x => x.SituacaoPagamento == ESituacaoPagamento.Pendente)
                        .Sum(x => x.RetornarTotal())
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-receber - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca da quantidade total de pedidos a fazer.
        /// </summary>
        /// <returns>Total de pedidos a fazer</returns>
        [HttpGet("total-fazer")]
        [Produces("application/json")]
        public IActionResult BuscarTotalAFazer()
        {
            try
            {
                List<PedidoDTO> lst = _pedidoService.GetAll(x => x.SituacaoPedido == ESituacaoPedido.AFazer)
                    .Select(x => (PedidoDTO)x)
                    .OrderByDescending(x => DateTime.ParseExact(x.DataCriacao, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    .ToList();

                return Ok(new TotalFazerResponse()
                {
                    Quantidade = lst.Count,
                    Pedidos = GetPedidoEnumerable(lst.Select(s => s).ToList())
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-fazer - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca do valor total de fluxo de caixa.
        /// </summary>
        /// <returns>Valor total de caixa</returns>
        [HttpGet("total-fluxocaixa")]
        [Produces("application/json")]
        public IActionResult BuscarTotalFluxoCaixa()
        {
            try
            {
                var lst = _movimentacaoService.GetAll();

                var totalDespesas= lst.Where(x => x.Tipo == ETipoMovimentacao.Despesa).Sum(x => x.Valor);
                var totalReceitas = lst.Where(x => x.Tipo == ETipoMovimentacao.Receita).Sum(x => x.Valor);
                var totalSaldo = totalReceitas - totalDespesas;

                return Ok(new TotalFluxoCaixaResponse()
                {
                    TotalDespesas = totalDespesas,
                    TotalReceitas = totalReceitas,
                    TotalSaldo = totalSaldo
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-fluxocaixa - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        private IEnumerable<object> GetPedidoEnumerable(List<PedidoDTO> lst)
        {
            return
                from e in lst.AsQueryable()
                select new
                {
                    e.Id,
                    Cliente = (ClienteDTO)_clienteService.Get(ObjectId.Parse(e.IdCliente)),
                    ListaProduto = 
                        from t in e.ListaProduto.AsQueryable()
                        select new
                        {
                            Produto = (ProdutoDTO)_produtoService.Get(ObjectId.Parse(t.IdProduto)),
                            t.Quantidade,
                            t.PrecoUnidade
                        },
                    e.Historico,
                    e.DataCriacao,
                    e.DataAtualizacao,
                    e.SituacaoPedido,
                    e.SituacaoPagamento,
                    e.Desconto,
                    e.Total
                };
        }
    }
}
