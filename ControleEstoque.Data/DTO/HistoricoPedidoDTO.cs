using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System;

namespace ControleEstoque.Data.DTO
{
    public class HistoricoPedidoDTO
    {
        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }
    }
}
