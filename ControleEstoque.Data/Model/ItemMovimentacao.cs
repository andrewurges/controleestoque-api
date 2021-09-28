﻿using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class ItemMovimentacao
    {
        [BsonElement("id_estoque")]
        public string IdEstoque { get; set; }

        [BsonElement("id_produto")]
        public string IdProduto { get; set; }

        [BsonElement("valor")]
        public double Valor { get; set; }

        [BsonElement("quantidade")]
        public int Quantidade { get; set; }

        public static implicit operator ItemMovimentacaoDTO(ItemMovimentacao model)
        {
            return new ItemMovimentacaoDTO()
            {
                IdEstoque = model.IdEstoque,
                IdProduto = model.IdProduto,
                Valor = model.Valor,
                Quantidade = model.Quantidade
            };
        }
    }
}
