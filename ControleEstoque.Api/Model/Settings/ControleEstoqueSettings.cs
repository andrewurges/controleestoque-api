namespace ControleEstoque.Api.Model
{
    public class ControleEstoqueSettings : IControleEstoqueSettings
    {
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }
    }
}
