﻿using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class IngredienteDTO
    {
        [JsonProperty("idEstoque")]
        public string IdEstoque { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
