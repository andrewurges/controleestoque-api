using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Desconto
    {
        [BsonElement("possui")]
        public bool Possui { get; set; }

        [BsonElement("tipo")]
        public ETipoDesconto Tipo { get; set; }

        [BsonElement("valor")]
        public double Valor { get; set; }

        public static implicit operator DescontoDTO(Desconto model)
        {
            return new DescontoDTO()
            {
                Possui = model.Possui,
                Tipo = model.Tipo,
                Valor = model.Valor
            };
        }
    }
}
