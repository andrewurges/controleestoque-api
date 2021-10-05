using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
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
        private readonly ILogger<EstoqueController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="produtoService"></param>
        /// /// <param name="pedidoService"></param>
        /// <param name="logger"></param>
        public DashboardController(ProdutoService produtoService,
            PedidoService pedidoService,
            ILogger<EstoqueController> logger)
        {
            _produtoService = produtoService;
            _pedidoService = pedidoService;
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
                        .Sum(x => x.ListaProduto.Sum(t => t.Quantidade * t.PrecoUnidade))
                });
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-receber - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca da quantidade total de pedidos por situação.
        /// </summary>
        /// <returns>Total de pedidos por situação</returns>
        [HttpGet("total-pedidos")]
        [Produces("application/json")]
        public IActionResult BuscarTotalPedidos()
        {
            try
            {
                var lst = _pedidoService.GetAll().ToList();
                var total = new List<TotalPedidoResponse>();

                foreach (ESituacaoPedido situacao in (ESituacaoPedido[])Enum.GetValues(typeof(ESituacaoPedido)))
                {
                    total.Add(new TotalPedidoResponse()
                    {
                        Situacao = situacao,
                        Quantidade = lst.FindAll(x => x.SituacaoPedido == situacao).Count()
                    });
                }

                return Ok(total);
            }
            catch (Exception e)
            {
                _logger.LogError($"dashboard/total-pedidos - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
