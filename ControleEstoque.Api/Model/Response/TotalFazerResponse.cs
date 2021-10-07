using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalFazerResponse
    {
        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("pedidos")]
        public IEnumerable<object> Pedidos { get; set; }
    }
}
