using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalReceberResponse
    {
        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
