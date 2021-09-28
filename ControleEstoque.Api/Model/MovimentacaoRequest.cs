using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class MovimentacaoRequest
    {
        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("itens")]
        public List<ItemMovimentacaoRequest> Itens { get; set; }
    }
}
