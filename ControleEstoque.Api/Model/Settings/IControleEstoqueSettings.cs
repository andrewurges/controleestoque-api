namespace ControleEstoque.Api.Model
{
    public interface IControleEstoqueSettings
    {
        string MongoConnectionString { get; set; }
        string MongoDatabaseName { get; set; }
    }
}
