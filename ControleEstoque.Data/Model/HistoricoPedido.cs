using ControleEstoque.Data.DTO;
using ControleEstoque.Data.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class HistoricoPedido
    {
        [BsonElement("situacao_pedido")]
        public ESituacaoPedido SituacaoPedido { get; set; }

        [BsonElement("data")]
        public string Data { get; set; }

        public static implicit operator HistoricoPedidoDTO(HistoricoPedido model)
        {
            return new HistoricoPedidoDTO()
            {
                SituacaoPedido = model.SituacaoPedido,
                Data = model.Data
            };
        }
    }
}
