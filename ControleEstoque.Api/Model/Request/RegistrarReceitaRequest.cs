using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model
{
    public class RegistrarReceitaRequest
    {
        [JsonProperty("idPedido")]
        public string IdPedido { get; set; }
    }
}
