using ControleEstoque.Data.Enum;
using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class DescontoDTO
    {
        [JsonProperty("tipo")]
        public ETipoDesconto Tipo { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }
    }
}
