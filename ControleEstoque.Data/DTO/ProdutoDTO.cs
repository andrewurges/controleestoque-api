namespace ControleEstoque.Data.DTO
{
    public class ProdutoDTO
    {
        public string id { get; set; }
        public string descricao { get; set; }
        public double preco { get; set; }
        public int quantidade_disponivel { get; set; }
    }
}
