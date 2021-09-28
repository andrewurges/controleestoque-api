using ControleEstoque.Data.DTO;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class ItemPedido
    {
        [BsonElement("id_produto")]
        public string IdProduto { get; set; }

        [BsonElement("quantidade")]
        public int Quantidade { get; set; }

        public static implicit operator ItemPedidoDTO(ItemPedido model)
        {
            return new ItemPedidoDTO()
            {
                IdProduto = model.IdProduto,
                Quantidade = model.Quantidade
            };
        }
    }
}
