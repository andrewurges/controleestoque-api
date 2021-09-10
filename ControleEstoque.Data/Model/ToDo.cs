using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ControleEstoque.Data.Model
{
    public class ToDo
    {
        public ToDo()
        {
            lista_produto = new List<string>();
        }

        [BsonId()]
        public ObjectId id { get; set; }

        [BsonElement("nome_cliente")]
        public string nome_cliente { get; set; }

        [BsonElement("lista_produto")]
        public List<string> lista_produto { get; set; }

        [BsonElement("data")]
        public DateTime data { get; set; }

        public static implicit operator ToDoDTO(ToDo model)
        {
            return new ToDoDTO()
            {
                id = model.id.ToString(),
                nome_cliente = model.nome_cliente,
                lista_produto = model.lista_produto,
                data = model.data
            };
        }
    }
}
