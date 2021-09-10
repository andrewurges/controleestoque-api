using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Consumo
    {
        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("agua")]
        public double agua { get; set; }

        [BsonElement("energia")]
        public double energia { get; set; }

        [BsonElement("mao_de_obra")]
        public double mao_de_obra { get; set; }

        public static implicit operator ConsumoDTO(Consumo model)
        {
            return new ConsumoDTO()
            {
                id = model.id.ToString(),
                agua = model.agua,
                energia = model.energia,
                mao_de_obra = model.mao_de_obra
            };
        }
    }
}
