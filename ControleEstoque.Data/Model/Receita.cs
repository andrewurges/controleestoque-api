using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace ControleEstoque.Data.Model
{
    public class Receita
    {
        public Receita()
        {
            ingredientes = new List<Ingrediente>();
        }

        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("id_produto")]
        public string id_produto { get; set; }

        [BsonElement("ingredientes")]
        public List<Ingrediente> ingredientes { get; set; }

        [BsonElement("modo_preparo")]
        public string modo_preparo { get; set; }

        public static implicit operator ReceitaDTO(Receita model)
        {
            return new ReceitaDTO()
            {
                id = model.id.ToString(),
                id_produto = model.id_produto,
                ingredientes = model.ingredientes.Select<Ingrediente, IngredienteDTO>(x => x).ToList(),
                modo_preparo = model.modo_preparo
            };
        }
    }
}
