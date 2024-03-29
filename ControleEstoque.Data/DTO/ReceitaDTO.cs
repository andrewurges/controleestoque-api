﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class ReceitaDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idProduto")]
        public string IdProduto { get; set; }

        [JsonProperty("ingredientes")]
        public List<IngredienteDTO> Ingredientes { get; set; }

        [JsonProperty("modoPreparo")]
        public string ModoPreparo { get; set; }
    }
}
