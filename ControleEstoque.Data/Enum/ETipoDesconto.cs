using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ETipoDesconto
    {
        [Description("Valor")] Valor = 0,
        [Description("Percentual")] Percentual = 1
    }
}
