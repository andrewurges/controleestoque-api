using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class HistoricoPedidoDTO
    {
        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
