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
    public class ClienteRepository : IControleEstoqueRepository<Cliente>
    {
        public static string collectionName = "Cliente";

        MongoConnection<Cliente> connection;

        public ClienteRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Cliente>(connectionString, databaseName, collectionName);
        }

        public Cliente Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.Id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Cliente();
            }
        }

        public List<Cliente> GetAll(Expression<Func<Cliente, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Cliente>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Cliente>();
            }
        }

        public Cliente Create(Cliente model)
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

        public Cliente Update(ObjectId id, Cliente model)
        {
            try
            {
                Expression<Func<Cliente, bool>> filter = x => x.Id.Equals(id);
                var update = new UpdateDefinitionBuilder<Cliente>()
                    .Set(n => n.NomeCompleto, model.NomeCompleto)
                    .Set(n => n.Telefone, model.Telefone)
                    .Set(n => n.Email, model.Email);

                var collection = connection.GetCollection();
                collection.FindOneAndUpdate(filter, update);

                return collection.Find(filter).FirstOrDefault();
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Cliente Delete(ObjectId id)
        {
            try
            {
                Expression<Func<Cliente, bool>> filter = x => x.Id.Equals(id);

                return connection.GetCollection().FindOneAndDelete(filter);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
