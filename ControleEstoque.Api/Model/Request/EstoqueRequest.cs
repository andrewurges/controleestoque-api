using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class EstoqueRequest
    {
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("unidadeMedida")]
        public EUnidadeMedida UnidadeMedida { get; set; }

        [JsonProperty("quantidadeDisponivel")]
        public int QuantidadeDisponivel { get; set; }
    }
}
