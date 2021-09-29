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
        /// <param name="requestBody">Objeto com os dados do consumo</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] ConsumoRequest requestBody)
        {
            try
            {
                return Ok((ConsumoDTO)_consumoService.Create(new Consumo()
                {
                    Agua = requestBody.Agua,
                    Energia = requestBody.Energia,
                    MaoDeObra = requestBody.MaoDeObra
                }));
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
        /// <param name="requestBody">Objeto com os dados do consumo</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] ConsumoRequest requestBody)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                consumo.Agua = requestBody.Agua;
                consumo.Energia = requestBody.Energia;
                consumo.MaoDeObra = requestBody.MaoDeObra;

                return Ok((ConsumoDTO)_consumoService.Update(ObjectId.Parse(id), consumo));
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
                if (_consumoService.Get(ObjectId.Parse(id)) == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                return Ok((ConsumoDTO)_consumoService.Delete(ObjectId.Parse(id)));
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
