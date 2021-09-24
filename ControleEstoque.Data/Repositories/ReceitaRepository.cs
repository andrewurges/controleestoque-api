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
    public class ReceitaRepository : IControleEstoqueRepository<Receita>
    {
        public static string collectionName = "Receita";

        MongoConnection<Receita> connection;

        public ReceitaRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Receita>(connectionString, databaseName, collectionName);
        }

        public Receita Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Receita();
            }
        }

        public List<Receita> GetAll(Expression<Func<Receita, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Receita>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Receita>();
            }
        }

        public bool Create(Receita model)
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

        public bool Update(ObjectId id, Receita model)
        {
            var status = false;
            try
            {
                Expression<Func<Receita, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Receita>()
                    .Set(n => n.id_produto, model.id_produto)
                    .Set(n => n.ingredientes, model.ingredientes)
                    .Set(n => n.modo_preparo, model.modo_preparo);

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
                Expression<Func<Receita, bool>> filter = x => x.id.Equals(id);

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
