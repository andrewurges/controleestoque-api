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
    ///     Controller para gerenciar os produtos do Controle de Estoque.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IControleEstoqueService<Produto> _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        /// <summary>
        ///     Construtor da Controller
        /// </summary>
        /// <param name="produtoService"></param>
        /// <param name="logger"></param>
        public ProdutoController(ProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        /// <summary>
        ///     Realiza a busca de todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos</returns>
        [HttpGet("listar")]
        [Produces("application/json")]
        public IActionResult ListarTodos()
        {
            try
            {
                List<ProdutoDTO> lst = _produtoService.GetAll().Select(x => (ProdutoDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/listar - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a busca de um produto através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        /// <returns>Objeto do produto</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                return Ok((ProdutoDTO)produto);
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/listar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo produto.
        /// </summary>
        /// <param name="model">Objeto com os dados do produto</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] Produto model)
        {
            try
            {
                if (!_produtoService.Create(model))
                    throw new Exception("Não foi possível incluir o produto.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/criar - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a atualização do produto do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        /// <param name="model">Objeto com os dados do produto</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] Produto model)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                if (!_produtoService.Update(ObjectId.Parse(id), model))
                    throw new Exception("Não foi possível atualizar o produto.");

                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/atualizar/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }

        /// <summary>
        ///     Realiza a remoção do produto através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                if (!_produtoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o produto.");

                return Ok(produto);
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/remover/{id} - {e.InnerException}");
                return BadRequest(e.InnerException);
            }
        }
    }
}
