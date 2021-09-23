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
    ///     Controller para gerenciar os consumos do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private IControleEstoqueService<Consumo> _consumoService;
        private readonly ILogger<ConsumoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="consumoService"></param>
        /// <param name="logger"></param>
        public ConsumoController(ConsumoService consumoService, ILogger<ConsumoController> logger)
        {
            _consumoService = consumoService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os consumos.
        /// </summary>
        /// <returns>Lista de consumos</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<ConsumoDTO> lst = _consumoService.GetAll().Select(x => (ConsumoDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de um consumo através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        /// <returns>Objeto do consumo</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                return Ok((ConsumoDTO)consumo);
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/listar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo consumo.
        /// </summary>
        /// <param name="model">Objeto com os dados do consumo</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] Consumo model)
        {
            try
            {
                if (!_consumoService.Create(model))
                    throw new Exception("Não foi possível incluir o consumo.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização do consumo do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        /// <param name="model">Objeto com os dados do consumo</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] Consumo model)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                if (!_consumoService.Update(ObjectId.Parse(id), model))
                    throw new Exception("Não foi possível atualizar o consumo.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a remoção do consumo através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                if (!_consumoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o consumo.");

                return Ok(consumo);
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
