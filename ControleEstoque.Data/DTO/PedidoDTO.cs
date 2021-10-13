using ControleEstoque.Data.Enum;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class PedidoDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idCliente")]
        public string IdCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<ItemPedidoDTO> ListaProduto { get; set; }

        [JsonProperty("historico")]
        public List<HistoricoPedidoDTO> Historico { get; set; }

        [JsonProperty("dataCriacao")]
        public string DataCriacao { get; set; }

        [JsonProperty("dataAtualizacao")]
        public string DataAtualizacao { get; set; }

        [JsonProperty("situacaoPedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [JsonProperty("situacaoPagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }

        [JsonProperty("desconto")]
        public DescontoDTO Desconto { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }
}
