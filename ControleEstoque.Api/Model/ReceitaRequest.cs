using ControleEstoque.Data.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class ReceitaRequest
    {
        [JsonProperty("idProduto")]
        public string IdProduto { get; set; }

        [JsonProperty("ingredientes")]
        public List<IngredienteRequest> Ingredientes { get; set; }

        [JsonProperty("modoPreparo")]
        public string ModoPreparo { get; set; }
    }
}
