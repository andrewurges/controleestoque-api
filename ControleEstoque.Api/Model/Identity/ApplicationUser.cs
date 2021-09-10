using AspNetCore.Identity.Mongo.Model;

namespace ControleEstoque.Api.Model.Identity
{
    public class ApplicationUser : MongoUser
    {
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
