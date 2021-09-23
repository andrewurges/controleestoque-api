using ControleEstoque.Data.Enum;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class PedidoDTO
    {
        public string id { get; set; }
        public string nome_cliente { get; set; }
        public List<string> lista_produto { get; set; }
        public DateTime data { get; set; }
        public ESituacaoPedido situacao_pedido { get; set; }
        public ESituacaoPagamento situacao_pagamento { get; set; }
    }
}
