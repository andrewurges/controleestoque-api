using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Estoque
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonElement("unidade_medida")]
        public EUnidadeMedida UnidadeMedida { get; set; }

        [BsonElement("quantidade_disponivel")]
        public int QuantidadeDisponivel { get; set; }

        public static implicit operator EstoqueDTO(Estoque model)
        {
            return new EstoqueDTO()
            {
                Id = model.Id.ToString(),
                Descricao = model.Descricao,
                UnidadeMedida = model.UnidadeMedida,
                QuantidadeDisponivel = model.QuantidadeDisponivel
            };
        }
    }
}
