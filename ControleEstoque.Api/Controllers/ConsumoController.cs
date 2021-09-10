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
        public RequestResponse ListarTodoConsumo()
        {
            try
            {
                List<Consumo> lst = _consumoService.GetAll();

                var query =
                    (from e in lst.AsQueryable<Consumo>()
                     select new ConsumoDTO(e)).ToList();

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = query
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/listar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a busca de um consumo através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        /// <returns>Objeto do consumo</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public RequestResponse ListarConsumo([FromRoute] string id)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = new ConsumoDTO(consumo)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/listar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo consumo.
        /// </summary>
        /// <param name="request">Objeto com os dados do consumo</param>
        [HttpPost("criar")]
        [Produces("application/json")]
        public RequestResponse CriarConsumo([FromBody] Consumo request)
        {
            try
            {
                if (!_consumoService.Create(request))
                    throw new Exception("Não foi possível incluir o consumo.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Consumo incluído com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/criar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a atualização do consumo do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        /// <param name="request">Objeto com os dados do consumo</param>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public RequestResponse AtualizarConsumo([FromRoute] string id, [FromBody] Consumo request)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                if (!_consumoService.Update(ObjectId.Parse(id), request))
                    throw new Exception("Não foi possível atualizar o consumo.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Consumo atualizado com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/atualizar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a remoção do consumo através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do consumo</param>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public RequestResponse RemoverConsumo([FromRoute] string id)
        {
            try
            {
                var consumo = _consumoService.Get(ObjectId.Parse(id));
                if (consumo == null)
                    throw new Exception($"Consumo com o ID {id} não foi encontrado.");

                if (!_consumoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o consumo.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Consumo removido com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"consumo/remover/{id} - {e.Message}");
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
