using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Consumo
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("agua")]
        public double Agua { get; set; }

        [BsonElement("energia")]
        public double Energia { get; set; }

        [BsonElement("mao_de_obra")]
        public double MaoDeObra { get; set; }

        public static implicit operator ConsumoDTO(Consumo model)
        {
            return new ConsumoDTO()
            {
                Id = model.Id.ToString(),
                Agua = model.Agua,
                Energia = model.Energia,
                MaoDeObra = model.MaoDeObra
            };
        }
    }
}
