using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalFluxoCaixaResponse
    {
        [JsonProperty("totalDespesas")]
        public double TotalDespesas { get; set; }

        [JsonProperty("totalReceitas")]
        public double TotalReceitas { get; set; }

        [JsonProperty("totalSaldo")]
        public double TotalSaldo { get; set; }
    }
}
