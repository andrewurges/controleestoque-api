using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ETipoMovimentacao
    {
        [Description("Despesa")] Despesa = 0,
        [Description("Receita")] Receita = 1
    }
}
