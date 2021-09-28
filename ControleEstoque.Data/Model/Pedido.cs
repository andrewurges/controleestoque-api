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
            ListaProduto = new List<string>();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("nome_cliente")]
        public string NomeCliente { get; set; }

        [BsonElement("lista_produto")]
        public List<string> ListaProduto { get; set; }

        [BsonElement("data")]
        public DateTime Data { get; set; }

        [BsonElement("situacao_pedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [BsonElement("situacao_pagamento")]
        public ESituacaoPagamento SituacaoPagamento { get; set; }

        public static implicit operator PedidoDTO(Pedido model)
        {
            return new PedidoDTO()
            {
                Id = model.Id.ToString(),
                NomeCliente = model.NomeCliente,
                ListaProduto = model.ListaProduto,
                Data = model.Data,
                SituacaoPedido = model.SituacaoPedido,
                SituacaoPagamento = model.SituacaoPagamento
            };
        }
    }
}
