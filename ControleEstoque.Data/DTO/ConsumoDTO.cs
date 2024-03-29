﻿using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class ConsumoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("agua")]
        public double Agua { get; set; }

        [JsonProperty("energia")]
        public double Energia { get; set; }

        [JsonProperty("maoDeObra")]
        public double MaoDeObra { get; set; }
    }
}
