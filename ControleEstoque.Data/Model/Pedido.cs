using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.Model
{
    public class Pedido
    {
        public Pedido()
        {
            lista_produto = new List<string>();
        }

        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("nome_cliente")]
        public string nome_cliente { get; set; }

        [BsonElement("lista_produto")]
        public List<string> lista_produto { get; set; }

        [BsonElement("data")]
        public DateTime data { get; set; }

        [BsonElement("situacao_pedido")]
        public ESituacaoPedido situacao_pedido { get; set; }

        [BsonElement("situacao_pagamento")]
        public ESituacaoPagamento situacao_pagamento { get; set; }

        public static implicit operator PedidoDTO(Pedido model)
        {
            return new PedidoDTO()
            {
                id = model.id.ToString(),
                nome_cliente = model.nome_cliente,
                lista_produto = model.lista_produto,
                data = model.data,
                situacao_pedido = model.situacao_pedido,
                situacao_pagamento = model.situacao_pagamento
            };
        }
    }
}
