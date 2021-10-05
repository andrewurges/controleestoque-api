using ControleEstoque.Data.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model.Response
{
    public class TotalEstoqueResponse
    {
        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("produtos")]
        public List<ProdutoDTO> Produtos { get; set; }
    }
}
