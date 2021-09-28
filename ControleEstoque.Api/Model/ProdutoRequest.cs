using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class ProdutoRequest
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("quantidadeDisponivel")]
        public int QuantidadeDisponivel { get; set; }
    }
}
