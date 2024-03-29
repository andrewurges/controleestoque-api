﻿using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class ProdutoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("quantidadeDisponivel")]
        public int QuantidadeDisponivel { get; set; }
    }
}
