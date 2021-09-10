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
    ///     Controller para gerenciar as receitas do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private IControleEstoqueService<Receita> _receitaService;
        private readonly ILogger<ReceitaController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="receitaService"></param>
        /// <param name="logger"></param>
        public ReceitaController(ReceitaService receitaService, ILogger<ReceitaController> logger)
        {
            _receitaService = receitaService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos as receitas.
        /// </summary>
        /// <returns>Lista de receitas</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public RequestResponse ListarTodoReceita()
        {
            try
            {
                List<Receita> lst = _receitaService.GetAll();

                var query =
                    (from e in lst.AsQueryable<Receita>()
                     select new ReceitaDTO(e)).ToList();

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = query
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/listar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a busca de uma receita através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        /// <returns>Objeto da receita</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public RequestResponse ListarReceita([FromRoute] string id)
        {
            try
            {
                var receita = _receitaService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = new ReceitaDTO(receita)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/listar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a criação de uma nova receita.
        /// </summary>
        /// <param name="request">Objeto com os dados da receita</param>
        [HttpPost("criar")]
        [Produces("application/json")]
        public RequestResponse CriarReceita([FromBody] Receita request)
        {
            try
            {
                if (!_receitaService.Create(request))
                    throw new Exception("Não foi possível incluir a receita.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Receita incluído com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/criar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a atualização da receita do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        /// <param name="request">Objeto com os dados da receita</param>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public RequestResponse AtualizarReceita([FromRoute] string id, [FromBody] Receita request)
        {
            try
            {
                var receita = _receitaService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                if (!_receitaService.Update(ObjectId.Parse(id), request))
                    throw new Exception("Não foi possível atualizar a receita.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Receita atualizado com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/atualizar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a remoção da receita através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public RequestResponse RemoverReceita([FromRoute] string id)
        {
            try
            {
                var receita = _receitaService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                if (!_receitaService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover a receita.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Receita removido com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/remover/{id} - {e.Message}");
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
