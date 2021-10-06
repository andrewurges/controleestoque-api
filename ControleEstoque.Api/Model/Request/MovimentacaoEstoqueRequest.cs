using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class MovimentacaoEstoqueRequest
    {
        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("itensEstoque")]
        public List<ItemMovimentacao> ItensEstoque { get; set; }
    }
}
