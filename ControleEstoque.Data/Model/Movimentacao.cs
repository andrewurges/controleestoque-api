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
            itens = new List<ItemMovimentacao>();
        }

        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("tipo")]
        public ETipoMovimentacao tipo { get; set; }

        [BsonElement("itens")]
        public List<ItemMovimentacao> itens { get; set; }

        public static implicit operator MovimentacaoDTO(Movimentacao model)
        {
            return new MovimentacaoDTO()
            {
                id = model.id.ToString(),
                tipo = model.tipo,
                itens = model.itens.Select<ItemMovimentacao, ItemMovimentacaoDTO>(x => x).ToList()
            };
        }
    }
}
