using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Ingrediente
    {
        [BsonElement("id_estoque")]
        public string IdEstoque { get; set; }

        [BsonElement("quantidade")]
        public int Quantidade { get; set; }

        public static implicit operator IngredienteDTO(Ingrediente model)
        {
            return new IngredienteDTO()
            {
                IdEstoque = model.IdEstoque,
                Quantidade = model.Quantidade
            };
        }
    }
}
