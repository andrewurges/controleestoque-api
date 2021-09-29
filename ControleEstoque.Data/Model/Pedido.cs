using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ControleEstoque.Data.Model
{
    public class Pedido
    {
        public Pedido()
        {
            ListaProduto = new List<ItemPedido>();
            Historico = new List<HistoricoPedido>();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("nome_cliente")]
        public string NomeCliente { get; set; }

        [BsonElement("lista_produto")]
        public List<ItemPedido> ListaProduto { get; set; }

        [BsonElement("historico")]
        public List<HistoricoPedido> Historico { get; set; }

        [BsonElement("data_criacao")]
        public string DataCriacao { get; set; }

        [BsonElement("data_atualizacao")]
        public string DataAtualizacao { get; set; }

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
                ListaProduto = model.ListaProduto.Select<ItemPedido, ItemPedidoDTO>(x => x).ToList(),
                Historico = model.Historico.Select<HistoricoPedido, HistoricoPedidoDTO>(x => x).ToList(),
                DataCriacao = model.DataCriacao,
                DataAtualizacao = model.DataAtualizacao,
                SituacaoPedido = model.SituacaoPedido,
                SituacaoPagamento = model.SituacaoPagamento
            };
        }
    }
}
