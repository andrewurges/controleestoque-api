using MongoDB.Driver;
using System.Security.Authentication;

namespace ControleEstoque.Data.Connection
{
    public class MongoConnection<T>
        where T : class
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        private string _dbName;

        public string DatabaseName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        private string _collectionName;

        public string CollectionName
        {
            get { return _collectionName; }
            set { _collectionName = value; }
        }

        public MongoConnection(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }

        public MongoConnection() { }

        public IMongoCollection<T> GetCollection()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);

            var database = client.GetDatabase(DatabaseName);
            var Collection = database.GetCollection<T>(CollectionName);
            return Collection;
        }
    }
}
