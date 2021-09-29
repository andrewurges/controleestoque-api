using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ESituacaoPagamento
    {
        [Description("Pagamento pendente")] Pendente = 0,
        [Description("Pago")] Pago = 1
    }
}
