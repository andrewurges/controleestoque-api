using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Api.Model.User
{
    public class AutenticarRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
