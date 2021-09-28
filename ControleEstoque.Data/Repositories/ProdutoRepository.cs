using ControleEstoque.Data.Connection;
using ControleEstoque.Data.Model;
using ControleEstoque.Data.Repositories.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ControleEstoque.Data.Repositories
{
    public class ProdutoRepository : IControleEstoqueRepository<Produto>
    {
        public static string collectionName = "Produto";

        MongoConnection<Produto> connection;

        public ProdutoRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Produto>(connectionString, databaseName, collectionName);
        }

        public Produto Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.Id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Produto();
            }
        }

        public List<Produto> GetAll(Expression<Func<Produto, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Produto>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Produto>();
            }
        }

        public Produto Create(Produto model)
        {
            try
            {
                var collection = connection.GetCollection();
                collection.InsertOne(model);

                return GetAll().Last();
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Produto Update(ObjectId id, Produto model)
        {
            try
            {
                Expression<Func<Produto, bool>> filter = x => x.Id.Equals(id);
                var update = new UpdateDefinitionBuilder<Produto>()
                    .Set(n => n.Descricao, model.Descricao)
                    .Set(n => n.Preco, model.Preco)
                    .Set(n => n.QuantidadeDisponivel, model.QuantidadeDisponivel);

                var collection = connection.GetCollection();
                collection.FindOneAndUpdate(filter, update);

                return collection.Find(filter).FirstOrDefault();
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Produto Delete(ObjectId id)
        {
            try
            {
                Expression<Func<Produto, bool>> filter = x => x.Id.Equals(id);

                return connection.GetCollection().FindOneAndDelete(filter);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
