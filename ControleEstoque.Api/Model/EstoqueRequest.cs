using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class EstoqueRequest
    {
        [JsonProperty("descricao")]
        [JsonRequired]
        public string Descricao { get; set; }

        [JsonProperty("preco")]
        [JsonRequired]
        public double Preco { get; set; }

        [JsonProperty("unidadeMedida")]
        [JsonRequired]
        public EUnidadeMedida UnidadeMedida { get; set; }

        [JsonProperty("quantidadeDisponivel")]
        [JsonRequired]
        public int QuantidadeDisponivel { get; set; }
    }
}
