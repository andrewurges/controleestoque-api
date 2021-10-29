using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleEstoque.Data.Model
{
    public class Movimentacao
    {
        public Movimentacao()
        {
            ItensEstoque = new List<ItemMovimentacao>();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [BsonElement("data")]
        public DateTime Data { get; set; }

        [BsonElement("id_pedido")]
        public string IdPedido { get; set; }

        [BsonElement("itens_estoque")]
        public List<ItemMovimentacao> ItensEstoque { get; set; }

        [BsonElement("valor")]
        public double Valor { get; set; }

        public static implicit operator MovimentacaoDTO(Movimentacao model)
        {
            return new MovimentacaoDTO()
            {
                Id = model.Id.ToString(),
                Tipo = model.Tipo,
                Data = model.Data,
                IdPedido = model.IdPedido,
                ItensEstoque = model.ItensEstoque.Select<ItemMovimentacao, ItemMovimentacaoDTO>(x => x).ToList(),
                Valor = model.Valor
            };
        }
    }
}
