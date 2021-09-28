﻿using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class ItemMovimentacaoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idEstoque")]
        public string IdEstoque { get; set; }

        [JsonProperty("idProduto")]
        public string IdProduto { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
