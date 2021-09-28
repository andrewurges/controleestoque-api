using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class ConsumoRequest
    {
        [JsonProperty("agua")]
        public double Agua { get; set; }

        [JsonProperty("energia")]
        public double Energia { get; set; }

        [JsonProperty("maoDeObra")]
        public double MaoDeObra { get; set; }
    }
}
