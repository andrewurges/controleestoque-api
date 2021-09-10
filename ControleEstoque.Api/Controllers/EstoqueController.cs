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
using System.Net;

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
        public IActionResult ListarTodoEstoque()
        {
            try
            {
                List<Estoque> lst = _estoqueService.GetAll();

                var query =
                    (from e in lst.AsQueryable<Estoque>()
                     select new EstoqueDTO(e)).ToList();

                return Ok(query);
                /*
                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = query
                };
                */
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/listar - {e.Message}");
                throw;
            }
        }

        /// <summary>
        ///     Realiza a busca de um estoque através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        /// <returns>Objeto do consumo</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public RequestResponse ListarEstoque([FromRoute] string id)
        {
            try
            {
                var estoque = _estoqueService.Get(ObjectId.Parse(id));
                if (estoque == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = new EstoqueDTO(estoque)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/listar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo estoque.
        /// </summary>
        /// <param name="request">Objeto com os dados do estoque</param>
        [HttpPost("criar")]
        [Produces("application/json")]
        public RequestResponse CriarEstoque([FromBody] Estoque request)
        {
            try
            {
                if (!_estoqueService.Create(request))
                    throw new Exception("Não foi possível incluir o estoque.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Estoque incluído com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/criar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a atualização do estoque do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        /// <param name="request">Objeto com os dados do estoque</param>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public RequestResponse AtualizarEstoque([FromRoute] string id, [FromBody] Estoque request)
        {
            try
            {
                var estoque = _estoqueService.Get(ObjectId.Parse(id));
                if (estoque == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                if (!_estoqueService.Update(ObjectId.Parse(id), request))
                    throw new Exception("Não foi possível atualizar o estoque.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Estoque atualizado com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/atualizar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a remoção do estoque através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do estoque</param>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public RequestResponse RemoverEstoque([FromRoute] string id)
        {
            try
            {
                var estoque = _estoqueService.Get(ObjectId.Parse(id));
                if (estoque == null)
                    throw new Exception($"Estoque com o ID {id} não foi encontrado.");

                if (!_estoqueService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o estoque.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Estoque removido com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"estoque/remover/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }
    }
}
