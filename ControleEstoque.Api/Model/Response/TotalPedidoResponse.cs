using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalPedidoResponse
    {
        [JsonProperty("totalPedidos")]
        public double TotalPedidos { get; set; }
    }
}
