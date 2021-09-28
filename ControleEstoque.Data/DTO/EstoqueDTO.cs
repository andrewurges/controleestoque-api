using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class EstoqueDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("preco")]
        public double Preco { get; set; }

        [JsonProperty("unidadeMedida")]
        public EUnidadeMedida UnidadeMedida { get; set; }

        [JsonProperty("quantidadeDisponivel")]
        public int QuantidadeDisponivel { get; set; }
    }
}
