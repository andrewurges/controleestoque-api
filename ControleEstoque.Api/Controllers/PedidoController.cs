using ControleEstoque.Api.Interface;
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
                _logger.LogError($"pedido/listar - {e.InnerException}");
                return BadRequest(e.InnerException);
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
                var todo = _pedidoService.Get(ObjectId.Parse(id));
                if (todo == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                return Ok((PedidoDTO)todo);
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/listar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo pedido.
        /// </summary>
        /// <param name="model">Objeto com os dados do pedido</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] Pedido model)
        {
            try
            {
                if (!_pedidoService.Create(model))
                    throw new Exception("Não foi possível incluir o pedido.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/criar - {e.InnerException}");
                return BadRequest(e.InnerException);
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
        public IActionResult Atualizar([FromRoute] string id, [FromBody] Pedido model)
        {
            try
            {
                var receita = _pedidoService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                if (!_pedidoService.Update(ObjectId.Parse(id), model))
                    throw new Exception("Não foi possível atualizar o pedido.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/atualizar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
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
                var todo = _pedidoService.Get(ObjectId.Parse(id));
                if (todo == null)
                    throw new Exception($"Pedido com o ID {id} não foi encontrado.");

                if (!_pedidoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o pedido.");

                return Ok(todo);
            }
            catch (Exception e)
            {
                _logger.LogError($"pedido/remover/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }
    }
}
