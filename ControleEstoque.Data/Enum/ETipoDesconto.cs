using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ETipoDesconto
    {
        [Description("Nenhum")] Nenhum = 0,
        [Description("Real")] Real = 1,
        [Description("Percentual")] Percentual = 2
    }
}
