using ControleEstoque.Data.Enum;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class MovimentacaoDTO
    {
        public string id { get; set; }
        public ETipoMovimentacao tipo { get; set; }
        public List<ItemMovimentacaoDTO> itens { get; set; }
    }
}
