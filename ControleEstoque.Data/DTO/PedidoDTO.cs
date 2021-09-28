using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class PedidoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("nomeCliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<ItemPedidoDTO> ListaProduto { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }
    }
}
