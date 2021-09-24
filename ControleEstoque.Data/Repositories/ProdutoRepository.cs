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
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
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

        public bool Create(Produto model)
        {
            var status = false;
            try
            {
                connection.GetCollection().InsertOne(model);
                status = true;
            }
            catch (MongoCommandException)
            {
                status = false;
            }
            return status;
        }

        public bool Update(ObjectId id, Produto model)
        {
            var status = false;
            try
            {
                Expression<Func<Produto, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Produto>()
                    .Set(n => n.descricao, model.descricao)
                    .Set(n => n.preco, model.preco)
                    .Set(n => n.quantidade_disponivel, model.quantidade_disponivel);

                if (connection.GetCollection().FindOneAndUpdate(filter, update) != null)
                {
                    status = true;
                }
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }

            return status;
        }

        public bool Delete(ObjectId id)
        {
            var status = false;
            try
            {
                Expression<Func<Produto, bool>> filter = x => x.id.Equals(id);

                if (connection.GetCollection().FindOneAndDelete(filter) != null)
                {
                    status = true;
                }
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }

            return status;
        }
    }
}
