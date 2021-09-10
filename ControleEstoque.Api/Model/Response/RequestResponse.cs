using System.Net;

namespace ControleEstoque.Api.Model
{
    public class RequestResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Mensagem { get; set; }
        public object Resultado { get; set; }
    }
}
