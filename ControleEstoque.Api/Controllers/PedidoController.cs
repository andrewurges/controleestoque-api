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
    ///     Controller para gerenciar os pedidos do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private IControleEstoqueService<Pedido> _pedidoService;
        private readonly ILogger<PedidoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="pedidoService"></param>
        /// <param name="logger"></param>
        public PedidoController(PedidoService pedidoService, ILogger<PedidoController> logger)
        {
            _pedidoService = pedidoService;
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
                List<PedidoDTO> lst = _pedidoService.GetAll().Select(x => (PedidoDTO)x).ToList();

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

                return Ok((PedidoDTO)pedido);
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
        /// <param name="model">Objeto com os dados do pedido</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] PedidoRequest model)
        {
            try
            {
                return Ok((PedidoDTO)_pedidoService.Create(new Pedido() 
                { 
                    NomeCliente = model.NomeCliente,
                    Data = model.Data,
                    ListaProduto = model.ListaProduto,
                    SituacaoPagamento = model.SituacaoPagamento,
                    SituacaoPedido = model.SituacaoPedido
                }));
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
        /// <param name="model">Objeto com os dados do pedido</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] PedidoRequest model)
        {
            try
            {
                var pedido = _pedidoService.Get(ObjectId.Parse(id));
                if (pedido == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                pedido.NomeCliente = model.NomeCliente;
                pedido.Data = model.Data;
                pedido.ListaProduto = model.ListaProduto;
                pedido.SituacaoPagamento = model.SituacaoPagamento;
                pedido.SituacaoPedido = model.SituacaoPedido;

                return Ok((PedidoDTO)_pedidoService.Update(ObjectId.Parse(id), pedido));
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a remoção do pedido através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do pedido</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                if (_pedidoService.Get(ObjectId.Parse(id)) == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                return Ok((PedidoDTO)_pedidoService.Delete(ObjectId.Parse(id)));
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
