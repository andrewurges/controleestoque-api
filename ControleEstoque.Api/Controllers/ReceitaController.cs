﻿using ControleEstoque.Api.Interface;
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
        public IActionResult ListarTodos()
        {
            try
            {
                List<ReceitaDTO> lst = _receitaService.GetAll().Select(x => (ReceitaDTO)x).ToList();

                return Ok(lst);
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/listar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a busca de uma receita através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        /// <returns>Objeto da receita</returns>
        [HttpGet("listar/{id}")]
        [Produces("application/json")]
        public IActionResult Listar([FromRoute] string id)
        {
            try
            {
                var receita = _receitaService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                return Ok((ReceitaDTO)receita);
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/listar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a criação de uma nova receita.
        /// </summary>
        /// <param name="requestBody">Objeto com os dados da receita</param>
        /// <returns>Objeto criado</returns>
        [HttpPost("criar")]
        [Produces("application/json")]
        public IActionResult Criar([FromBody] ReceitaRequest requestBody)
        {
            try
            {
                return Ok((ReceitaDTO)_receitaService.Create(new Receita() 
                { 
                    IdProduto = requestBody.IdProduto,
                    ModoPreparo = requestBody.ModoPreparo,
                    Ingredientes = requestBody.Ingredientes
                }));
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/criar - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a atualização da receita do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        /// <param name="requestBody">Objeto com os dados da receita</param>
        /// <returns>Objeto atualizado</returns>
        [HttpPost("atualizar/{id}")]
        [Produces("application/json")]
        public IActionResult Atualizar([FromRoute] string id, [FromBody] ReceitaRequest requestBody)
        {
            try
            {
                var receita = _receitaService.Get(ObjectId.Parse(id));
                if (receita == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                receita.IdProduto = requestBody.IdProduto;
                receita.ModoPreparo = requestBody.ModoPreparo;
                receita.Ingredientes = requestBody.Ingredientes;

                return Ok((ReceitaDTO)_receitaService.Update(ObjectId.Parse(id), receita));
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/atualizar/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///     Realiza a remoção da receita através do ID informado.
        /// </summary>
        /// <param name="id">Código identificador da receita</param>
        /// <returns>Objeto removido</returns>
        [HttpDelete("remover/{id}")]
        [Produces("application/json")]
        public IActionResult Remover([FromRoute] string id)
        {
            try
            {
                if (_receitaService.Get(ObjectId.Parse(id)) == null)
                    throw new Exception($"Receita com o ID {id} não foi encontrado.");

                return Ok((ReceitaDTO)_receitaService.Delete(ObjectId.Parse(id)));
            }
            catch (Exception e)
            {
                _logger.LogError($"receita/remover/{id} - {e.Message}");
                return BadRequest(e.Message);
            }
        }
    }
}
