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
    ///     Controller para gerenciar os pedidos do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private IControleEstoqueService<Pedido> _pedidoService;
        private IControleEstoqueService<Produto> _produtoService;
        private readonly ILogger<PedidoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="pedidoService"></param>
        /// <param name="produtoService"></param>
        /// <param name="logger"></param>
        public PedidoController(PedidoService pedidoService, 
            ProdutoService produtoService,
            ILogger<PedidoController> logger)
        {
            _pedidoService = pedidoService;
            _produtoService = produtoService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os pedidos.
        /// </summary>
        /// <returns>Lista de pedidos</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<PedidoDTO> lst = _pedidoService.GetAll().Select(x => (PedidoDTO)x)
                    .OrderByDescending(x => DateTime.Parse(x.DataCriacao))
                    .ToList();

                return Ok(GetPedidoEnumerable(lst));
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de todos os pedidos agrupado por data de criação.
        /// </summary>
        /// <returns>Lista de pedidos agrupada</returns>
        [HttpGet("listar-agrupado")]
        [Produces("application/json")]
        public IActionResult ListarAgrupado()
        {
            try
            {
                var agrupado = _pedidoService.GetAll().Select(x => (PedidoDTO)x)
                    .OrderByDescending(x => DateTime.ParseExact(x.DataCriacao, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                    .GroupBy(x => x.DataCriacao)
                    .ToList();

                var lst = agrupado.Select(x => new
                {
                    Data = x.Key,
                    Pedidos = GetPedidoEnumerable(x.Select(s => s).ToList())
                });

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de um pedido através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do pedido</param>
        /// <returns>Objeto do pedido</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var pedido = _pedidoService.Get(ObjectId.Parse(id));
                if (pedido == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                var lst = new List<PedidoDTO>() { pedido };

                return Ok(GetPedidoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/listar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo pedido.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados do pedido</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] CriarPedidoRequest requestBody)
        {
            try
            {
                var novoPedido = (PedidoDTO)_pedidoService.Create(new Pedido()
                {
                    NomeCliente = requestBody.NomeCliente,
                    ListaProduto = requestBody.ListaProduto,
                    SituacaoPagamento = requestBody.SituacaoPagamento
                });

                var lst = new List<PedidoDTO>() { novoPedido };

                return Ok(GetPedidoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização do pedido do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do pedido</param>
        /// <param name="requestBody">Objeto com os dados do pedido</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] AtualizarPedidoRequest requestBody)
        {
            try
            {
                var pedido = _pedidoService.Get(ObjectId.Parse(id));
                if (pedido == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                pedido.NomeCliente = requestBody.NomeCliente;
                pedido.ListaProduto = requestBody.ListaProduto;
                pedido.SituacaoPagamento = requestBody.SituacaoPagamento;
                pedido.SituacaoPedido = requestBody.SituacaoPedido;

                var pedidoAtualizado = (PedidoDTO)_pedidoService.Update(ObjectId.Parse(id), pedido);
                var lst = new List<PedidoDTO>() { pedidoAtualizado };

                return Ok(GetPedidoEnumerable(lst).FirstOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        private IEnumerable<object> GetPedidoEnumerable(List<PedidoDTO> lst)
        {
            return from e in lst.AsQueryable()
                   select new
                   {
                       e.Id,
                       e.NomeCliente,
                       ListaProduto = from t in e.ListaProduto.AsQueryable()
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
                       e.SituacaoPagamento
                   };
        }
    }
}
