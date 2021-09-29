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

        [BsonElement("preco_unidade")]
        public double PrecoUnidade { get; set; }

        public static implicit operator ItemPedidoDTO(ItemPedido model)
        {
            return new ItemPedidoDTO()
            {
                IdProduto = model.IdProduto,
                Quantidade = model.Quantidade,
                PrecoUnidade = model.PrecoUnidade
            };
        }
    }
}
