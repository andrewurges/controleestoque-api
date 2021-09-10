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
    ///     Controller para gerenciar o To-Do do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private IControleEstoqueService<ToDo> _todoService;
        private readonly ILogger<ToDoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="todoService"></param>
        /// <param name="logger"></param>
        public ToDoController(ToDoService todoService, ILogger<ToDoController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os itens do To-Do.
        /// </summary>
        /// <returns>Lista de To-Do</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<ToDoDTO> lst = _todoService.GetAll().Select(x => (ToDoDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"todo/listar - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a busca de um item do To-Do através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do To-Do</param>
        /// <returns>Objeto da receita</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var todo = _todoService.Get(ObjectId.Parse(id));
                if (todo == null)
                    throw new Exception($"To-Do com o ID {id} não foi encontrado.");

                return Ok((ToDoDTO)todo);
            }
            catch (Exception e)
            {
                _logger.LogError($"todo/listar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo To-Do.
        /// </summary>
        /// <param name="model">Objeto com os dados do To-Do</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] ToDo model)
        {
            try
            {
                if (!_todoService.Create(model))
                    throw new Exception("Não foi possível incluir a receita.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/criar - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a atualização do To-Do do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do To-Do</param>
        /// <param name="model">Objeto com os dados do To-Do</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] ToDo model)
        {
            try
            {
                var receita = _todoService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"To-Do com o ID {id} não foi encontrado.");

                if (!_todoService.Update(ObjectId.Parse(id), model))
                    throw new Exception("Não foi possível atualizar o To-Do.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"todo/atualizar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a remoção do To-Do através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do To-Do</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                var todo = _todoService.Get(ObjectId.Parse(id));
                if (todo == null)
                    throw new Exception($"To-Do com o ID {id} não foi encontrado.");

                if (!_todoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o To-Do.");

                return Ok(todo);
            }
            catch (Exception e)
            {
                _logger.LogError($"todo/remover/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }
    }
}
