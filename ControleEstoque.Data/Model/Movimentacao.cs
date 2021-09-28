using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ControleEstoque.Data.Model
{
    public class Movimentacao
    {
        public Movimentacao()
        {
            Itens = new List<ItemMovimentacao>();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("tipo")]
        public ETipoMovimentacao Tipo { get; set; }

        [BsonElement("data")]
        public string Data { get; set; }

        [BsonElement("itens")]
        public List<ItemMovimentacao> Itens { get; set; }

        public static implicit operator MovimentacaoDTO(Movimentacao model)
        {
            return new MovimentacaoDTO()
            {
                Id = model.Id.ToString(),
                Tipo = model.Tipo,
                Data = model.Data,
                Itens = model.Itens.Select<ItemMovimentacao, ItemMovimentacaoDTO>(x => x).ToList()
            };
        }
    }
}
