using Newtonsoft.Json;

namespace ControleEstoque.Data.DTO
{
    public class ItemPedidoDTO
    {
        [JsonProperty("idProduto")]
        public string IdProduto { get; set; }

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }
    }
}
