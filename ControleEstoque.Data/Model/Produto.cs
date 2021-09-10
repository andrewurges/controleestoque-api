using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Produto
    {
        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("descricao")]
        public string descricao { get; set; }

        [BsonElement("foto")]
        public string foto { get; set; }

        [BsonElement("preco")]
        public double preco { get; set; }

        [BsonElement("quantidade_disponivel")]
        public int quantidade_disponivel { get; set; }

        public static implicit operator ProdutoDTO(Produto model)
        {
            return new ProdutoDTO()
            {
                id = model.id.ToString(),
                descricao = model.descricao,
                foto = model.foto,
                preco = model.preco,
                quantidade_disponivel = model.quantidade_disponivel
            };
        }
    }
}
