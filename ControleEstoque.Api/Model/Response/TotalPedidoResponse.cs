using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalPedidoResponse
    {
        [JsonProperty("situacao")]
        public ESituacaoPedido Situacao { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
