using ControleEstoque.Data.Enum;

namespace ControleEstoque.Data.DTO
{
    public class EstoqueDTO
    {
        public string id { get; set; }
        public string descricao { get; set; }
        public double preco { get; set; }
        public EUnidadeMedida unidade_medida { get; set; }
        public int quantidade_disponivel { get; set; }
    }
}
