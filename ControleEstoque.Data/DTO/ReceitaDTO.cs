using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class ReceitaDTO
    {
        public string id { get; set; }
        public string id_produto { get; set; }
        public List<IngredienteDTO> ingredientes { get; set; }
        public string modo_preparo { get; set; }
    }
}
