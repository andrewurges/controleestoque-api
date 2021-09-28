using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class MovimentacaoRequest
    {
        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("itens")]
        public List<ItemMovimentacao> Itens { get; set; }
    }
}
