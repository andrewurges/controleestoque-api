using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class AtualizarPedidoRequest
    {
        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }
    }
}
