using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Ingrediente
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("id_estoque")]
        public string IdEstoque { get; set; }

        [BsonElement("quantidade")]
        public int Quantidade { get; set; }

        public static implicit operator IngredienteDTO(Ingrediente model)
        {
            return new IngredienteDTO()
            {
                Id = model.Id.ToString(),
                IdEstoque = model.IdEstoque,
                Quantidade = model.Quantidade
            };
        }
    }
}
