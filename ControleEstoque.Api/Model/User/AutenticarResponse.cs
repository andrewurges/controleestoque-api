namespace ControleEstoque.Api.Model.User
{
    public class AutenticarResponse
    {
        public AutenticarResponse(string token, string userName, string fullName)
        {
            Token = token;
            UserName = userName;
            FullName = fullName;
        }

        public string Token { get; private set; }
        public string UserName { get; private set; }
        public string FullName { get; set; }
    }
}
