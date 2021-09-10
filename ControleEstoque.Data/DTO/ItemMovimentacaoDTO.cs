namespace ControleEstoque.Data.DTO
{
    public class ItemMovimentacaoDTO
    {
        public string id { get; set; }
        public string id_estoque { get; set; }
        public string id_produto { get; set; }
        public double valor { get; set; }
        public int quantidade { get; set; }
    }
}
