using ControleEstoque.Data.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleEstoque.Data.Model
{
    public class Cliente
    {
        public Cliente()
        {
            DescontoPadrao = new Desconto();
        }

        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("nome_completo")]
        public string NomeCompleto { get; set; }

        [BsonElement("telefone")]
        public string Telefone { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("desconto_padrao")]
        public Desconto DescontoPadrao { get; set; }

        public static implicit operator ClienteDTO(Cliente model)
        {
            return new ClienteDTO()
            {
                Id = model.Id.ToString(),
                NomeCompleto = model.NomeCompleto,
                Telefone = model.Telefone,
                Email = model.Email,
                DescontoPadrao = model.DescontoPadrao
            };
        }
    }
}
