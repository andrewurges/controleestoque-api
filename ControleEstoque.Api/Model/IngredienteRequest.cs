using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class IngredienteRequest
    {
        [JsonProperty("idEstoque")]
        public string IdEstoque { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
