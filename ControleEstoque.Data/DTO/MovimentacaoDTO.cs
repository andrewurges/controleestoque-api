using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class MovimentacaoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("idPedido")]
        public string IdPedido { get; set; }

        [JsonProperty("itensEstoque")]
        public List<ItemMovimentacaoDTO> ItensEstoque { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }
    }
}
