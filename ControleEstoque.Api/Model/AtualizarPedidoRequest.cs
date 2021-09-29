using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class AtualizarPedidoRequest
    {
        [JsonProperty("nomeCliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<ItemPedido> ListaProduto { get; set; }

        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }
    }
}
