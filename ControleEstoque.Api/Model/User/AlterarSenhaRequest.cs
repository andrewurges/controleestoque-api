namespace ControleEstoque.Api.Model.User
{
    public class AlterarSenhaRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
