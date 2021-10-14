using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Desconto
    {
        [BsonElement("tipo")]
        public ETipoDesconto Tipo { get; set; }

        [BsonElement("valor")]
        public double Valor { get; set; }

        public static implicit operator DescontoDTO(Desconto model)
        {
            return new DescontoDTO()
            {
                Tipo = model.Tipo,
                Valor = model.Valor
            };
        }
    }
}
