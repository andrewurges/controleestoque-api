using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class ClienteDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nomeCompleto")]
        public string NomeCompleto { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("descontoPadrao")]
        public DescontoDTO DescontoPadrao { get; set; }
    }
}
