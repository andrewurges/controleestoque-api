using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class ItemMovimentacaoRequest
    {
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
