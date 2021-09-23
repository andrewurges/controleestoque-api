using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ESituacaoPedido
    {
        [Description("A fazer")] AFazer = 0,
        [Description("Em produção")] EmProducao = 1,
        [Description("Finalizado")] Finalizado = 2
    }
}
