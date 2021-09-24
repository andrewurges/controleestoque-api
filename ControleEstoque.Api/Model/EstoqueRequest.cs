using ControleEstoque.Data.Enum;

namespace ControleEstoque.Api.Model
{
    public class EstoqueRequest
    {
        public string descricao { get; set; }
        public double preco { get; set; }
        public EUnidadeMedida unidade_medida { get; set; }
        public int quantidade_disponivel { get; set; }
    }
}
