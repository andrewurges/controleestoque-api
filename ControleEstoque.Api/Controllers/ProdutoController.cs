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
        public RequestResponse ListarTodoProduto()
        {
            try
            {
                List<Produto> lst = _produtoService.GetAll();

                var query =
                    (from e in lst.AsQueryable<Produto>()
                     select new ProdutoDTO(e)).ToList();

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = query
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/listar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a busca de um produto através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        /// <returns>Objeto do produto</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public RequestResponse ListarProduto([FromRoute] string id)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = string.Empty,
                    Resultado = new ProdutoDTO(produto)
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/listar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a criação de um novo produto.
        /// </summary>
        /// <param name="request">Objeto com os dados do produto</param>
        [HttpPost("criar")]
        [Produces("application/json")]
        public RequestResponse CriarProduto([FromBody] Produto request)
        {
            try
            {
                if (!_produtoService.Create(request))
                    throw new Exception("Não foi possível incluir o produto.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Produto incluído com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/criar - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a atualização do produto do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        /// <param name="request">Objeto com os dados do produto</param>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public RequestResponse AtualizarProduto([FromRoute] string id, [FromBody] Produto request)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                if (!_produtoService.Update(ObjectId.Parse(id), request))
                    throw new Exception("Não foi possível atualizar o produto.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Produto atualizado com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/atualizar/{id} - {e.Message}");
                return new RequestResponse()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Mensagem = e.Message,
                    Resultado = null
                };
            }
        }

        /// <summary>
        ///     Realiza a remoção do produto através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador do produto</param>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public RequestResponse RemoverProduto([FromRoute] string id)
        {
            try
            {
                var produto = _produtoService.Get(ObjectId.Parse(id));
                if (produto == null)
                    throw new Exception($"Produto com o ID {id} não foi encontrado.");

                if (!_produtoService.Delete(ObjectId.Parse(id)))
                    throw new Exception("Não foi possível remover o produto.");

                return new RequestResponse()
                {
                    Status = HttpStatusCode.OK,
                    Mensagem = "Produto removido com sucesso!",
                    Resultado = null
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"produto/remover/{id} - {e.Message}");
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
