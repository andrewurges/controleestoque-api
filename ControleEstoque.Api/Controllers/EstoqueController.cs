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
    ///     Controller para gerenciar os estoques do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class EstoqueController : ControllerBase
    {
        private IControleEstoqueService<Estoque> _estoqueService;
        private readonly ILogger<EstoqueController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="estoqueService"></param>
        /// <param name="logger"></param>
        public EstoqueController(EstoqueService estoqueService, ILogger<EstoqueController> logger)
        {
            _estoqueService = estoqueService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os estoques.
        /// </summary>
        /// <returns>Lista de estoques</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<EstoqueDTO> lst = _estoqueService.GetAll().Select(x => (EstoqueDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de um estoque através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        /// <returns>Objeto do consumo</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var estoque = _estoqueService.Get(ObjectId.Parse(id));
                if (estoque == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                return Ok((EstoqueDTO)estoque);
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/listar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo estoque.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados do estoque</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] EstoqueRequest requestBody)
        {
            try
            {
                return Ok((EstoqueDTO)_estoqueService.Create(new Estoque()
                {
                    Descricao = requestBody.Descricao,
                    QuantidadeDisponivel = requestBody.QuantidadeDisponivel,
                    UnidadeMedida = requestBody.UnidadeMedida
                }));
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização do estoque do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        /// <param name="requestBody">Objeto com os dados do estoque</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] EstoqueRequest requestBody)
        {
            try
            {
                var estoque = _estoqueService.Get(ObjectId.Parse(id));
                if (estoque == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                estoque.Descricao = requestBody.Descricao;
                estoque.UnidadeMedida = requestBody.UnidadeMedida;
                estoque.QuantidadeDisponivel = requestBody.QuantidadeDisponivel;

                return Ok((EstoqueDTO)_estoqueService.Update(ObjectId.Parse(id), estoque));
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a remoção do estoque através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                if (_estoqueService.Get(ObjectId.Parse(id)) == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                return Ok((EstoqueDTO)_estoqueService.Delete(ObjectId.Parse(id)));
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
