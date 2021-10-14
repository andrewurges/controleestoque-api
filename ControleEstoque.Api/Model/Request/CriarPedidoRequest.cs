using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class CriarPedidoRequest
    {
        [JsonProperty("idCliente")]
        public string IdCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<ItemPedido> ListaProduto { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }

        [JsonProperty("tipoDesconto")]
        public ETipoDesconto TipoDesconto { get; set; }

        [JsonProperty("valorDesconto")]
        public double ValorDesconto { get; set; }
    }
}
