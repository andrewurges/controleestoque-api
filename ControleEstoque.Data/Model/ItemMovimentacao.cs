using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class ItemMovimentacao
    {
        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("id_estoque")]
        public string id_estoque { get; set; }

        [BsonElement("id_produto")]
        public string id_produto { get; set; }

        [BsonElement("valor")]
        public double valor { get; set; }

        [BsonElement("quantidade")]
        public int quantidade { get; set; }

        public static implicit operator ItemMovimentacaoDTO(ItemMovimentacao model)
        {
            return new ItemMovimentacaoDTO()
            {
                id = model.id.ToString(),
                id_estoque = model.id_estoque,
                id_produto = model.id_produto,
                valor = model.valor,
                quantidade = model.quantidade
            };
        }
    }
}
