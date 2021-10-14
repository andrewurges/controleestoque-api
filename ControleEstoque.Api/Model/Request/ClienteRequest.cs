using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Api.Model.Request
{
    public class ClienteRequest
    {
        [JsonProperty("nomeCompleto")]
        public string NomeCompleto { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("tipoDescontoPadrao")]
        public ETipoDesconto TipoDescontoPadrao { get; set; }

        [JsonProperty("valorDescontoPadrao")]
        public double ValorDescontoPadrao { get; set; }
    }
}
