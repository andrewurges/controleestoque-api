using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Estoque
    {
        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("descricao")]
        public string descricao { get; set; }

        [BsonElement("foto")]
        public string foto { get; set; }

        [BsonElement("preco")]
        public double preco { get; set; }

        [BsonElement("unidade_medida")]
        public EUnidadeMedida unidade_medida { get; set; }

        [BsonElement("quantidade_disponivel")]
        public int quantidade_disponivel { get; set; }

        public static implicit operator EstoqueDTO(Estoque model)
        {
            return new EstoqueDTO()
            {
                id = model.id.ToString(),
                descricao = model.descricao,
                foto = model.foto,
                preco = model.preco,
                unidade_medida = model.unidade_medida,
                quantidade_disponivel = model.quantidade_disponivel
            };
        }
    }
}
