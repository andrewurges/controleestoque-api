using ControleEstoque.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControleEstoque.Api.Model
{
    public class CriarPedidoRequest
    {
        [JsonProperty("nomeCliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("listaProduto")]
        public List<ItemPedido> ListaProduto { get; set; }
    }
}
