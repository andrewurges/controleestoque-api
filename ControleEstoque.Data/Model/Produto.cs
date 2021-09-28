using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Produto
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonElement("preco")]
        public double Preco { get; set; }

        [BsonElement("quantidade_disponivel")]
        public int QuantidadeDisponivel { get; set; }

        public static implicit operator ProdutoDTO(Produto model)
        {
            return new ProdutoDTO()
            {
                Id = model.Id.ToString(),
                Descricao = model.Descricao,
                Preco = model.Preco,
                QuantidadeDisponivel = model.QuantidadeDisponivel
            };
        }
    }
}
