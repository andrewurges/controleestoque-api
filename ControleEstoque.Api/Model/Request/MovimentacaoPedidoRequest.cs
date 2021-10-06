using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class MovimentacaoPedidoRequest
    {
        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("idPedido")]
        public string IdPedido { get; set; }
    }
}
