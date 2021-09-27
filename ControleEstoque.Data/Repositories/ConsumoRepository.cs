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
    public class ConsumoRepository : IControleEstoqueRepository<Consumo>
    {
        public static string collectionName = "Consumo";

        MongoConnection<Consumo> connection;

        public ConsumoRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Consumo>(connectionString, databaseName, collectionName);
        }

        public Consumo Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Consumo();
            }
        }

        public List<Consumo> GetAll(Expression<Func<Consumo, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Consumo>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Consumo>();
            }
        }

        public Consumo Create(Consumo model)
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

        public Consumo Update(ObjectId id, Consumo model)
        {
            try
            {
                Expression<Func<Consumo, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Consumo>()
                    .Set(n => n.agua, model.agua)
                    .Set(n => n.energia, model.energia)
                    .Set(n => n.mao_de_obra, model.mao_de_obra);


                return connection.GetCollection().FindOneAndUpdate(filter, update);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Consumo Delete(ObjectId id)
        {
            try
            {
                Expression<Func<Consumo, bool>> filter = x => x.id.Equals(id);

                return connection.GetCollection().FindOneAndDelete(filter);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
