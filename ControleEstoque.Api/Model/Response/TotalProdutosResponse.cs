using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalProdutosResponse
    {
        [JsonProperty("totalProdutos")]
        public double TotalProdutos { get; set; }
    }
}
