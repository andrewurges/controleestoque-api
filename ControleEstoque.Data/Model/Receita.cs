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
            Ingredientes = new List<Ingrediente>();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("id_produto")]
        public string IdProduto { get; set; }

        [BsonElement("ingredientes")]
        public List<Ingrediente> Ingredientes { get; set; }

        [BsonElement("modo_preparo")]
        public string ModoPreparo { get; set; }

        public static implicit operator ReceitaDTO(Receita model)
        {
            return new ReceitaDTO()
            {
                Id = model.Id.ToString(),
                IdProduto = model.IdProduto,
                Ingredientes = model.Ingredientes.Select<Ingrediente, IngredienteDTO>(x => x).ToList(),
                ModoPreparo = model.ModoPreparo
            };
        }
    }
}
