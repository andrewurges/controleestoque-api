using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalMovimentacaoResponse
    {
        [JsonProperty("totalEntrada")]
        public double TotalEntrada { get; set; }

        [JsonProperty("totalSaida")]
        public double TotalSaida { get; set; }

        [JsonProperty("totalLucroLiquido")]
        public double TotalLucroLiquido { get; set; }
    }
}
