using System.ComponentModel;

namespace ControleEstoque.Data.Enum
{
    public enum ESituacaoPedido
    {
        [Description("A fazer")] AFazer = 0,         //Azul
        [Description("Em produção")] EmProducao = 1, //Azul
        [Description("Finalizado")] Finalizado = 2,  //Azul
        [Description("Entregue")] Entregue = 3,      //Verde
        [Description("Cancelado")] Cancelado = 4,    //Vermelho
    }
}
