using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class PedidoRequest
    {
        [JsonProperty("nomeCliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<string> ListaProduto { get; set; }

        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }
    }
}
