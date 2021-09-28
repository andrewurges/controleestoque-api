using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class MovimentacaoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("itens")]
        public List<ItemMovimentacaoDTO> Itens { get; set; }
    }
}
