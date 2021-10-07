using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Api.Model.Request;
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
    ///     Controller para gerenciar os clientes do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IControleEstoqueService<Cliente> _clienteService;
        private readonly ILogger<ClienteController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="clienteService"></param>
        /// <param name="logger"></param>
        public ClienteController(ClienteService clienteService, ILogger<ClienteController> logger)
        {
            _clienteService = clienteService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os clientes.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<ClienteDTO> lst = _clienteService.GetAll().Select(x => (ClienteDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"cliente/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de um cliente através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do cliente</param>
        /// <returns>Objeto do cliente</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var consumo = _clienteService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Cliente com o ID {id} não foi encontrado.");

                return Ok((ClienteDTO)consumo);
            }
            catch (Exception e)
            {
                _logger.LogError($"cliente/listar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo cliente.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados do ccliente</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] ClienteRequest requestBody)
        {
            try
            {
                return Ok((ClienteDTO)_clienteService.Create(new Cliente()
                {
                    NomeCompleto = requestBody.NomeCompleto,
                    Telefone = requestBody.Telefone,
                    Email = requestBody.Email
                }));
            }
            catch (Exception e)
            {
                _logger.LogError($"cliente/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização do cliente do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do cliente</param>
        /// <param name="requestBody">Objeto com os dados do cliente</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] ClienteRequest requestBody)
        {
            try
            {
                var cliente = _clienteService.Get(ObjectId.Parse(id));
                if (cliente == null)
                    throw new Exception($"Cliente com o ID {id} não foi encontrado.");

                cliente.NomeCompleto = requestBody.NomeCompleto;
                cliente.Telefone = requestBody.Telefone;
                cliente.Email = requestBody.Email;

                return Ok((ClienteDTO)_clienteService.Update(ObjectId.Parse(id), cliente));
            }
            catch (Exception e)
            {
                _logger.LogError($"cliente/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a remoção do cliente através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do cliente</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                if (_clienteService.Get(ObjectId.Parse(id)) == null)
                    throw new Exception($"Cliente com o ID {id} não foi encontrado.");

                return Ok((ClienteDTO)_clienteService.Delete(ObjectId.Parse(id)));
            }
            catch (Exception e)
            {
                _logger.LogError($"cliente/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
