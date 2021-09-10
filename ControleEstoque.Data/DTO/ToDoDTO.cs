using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.DTO
{
    public class ToDoDTO
    {
        public string id { get; set; }
        public string nome_cliente { get; set; }
        public List<string> lista_produto { get; set; }
        public DateTime data { get; set; }
    }
}
