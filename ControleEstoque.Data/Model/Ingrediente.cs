using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Ingrediente
    {
        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("id_estoque")]
        public string id_estoque { get; set; }

        [BsonElement("quantidade")]
        public int quantidade { get; set; }

        public static implicit operator IngredienteDTO(Ingrediente model)
        {
            return new IngredienteDTO()
            {
                id = model.id.ToString(),
                id_estoque = model.id_estoque,
                quantidade = model.quantidade
            };
        }
    }
}
