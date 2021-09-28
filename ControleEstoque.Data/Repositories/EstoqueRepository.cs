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
    public class EstoqueRepository : IControleEstoqueRepository<Estoque>
    {
        public static string collectionName = "Estoque";

        MongoConnection<Estoque> connection;

        public EstoqueRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Estoque>(connectionString, databaseName, collectionName);
        }

        public Estoque Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Estoque();
            }
        }

        public List<Estoque> GetAll(Expression<Func<Estoque, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Estoque>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Estoque>();
            }
        }

        public Estoque Create(Estoque model)
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

        public Estoque Update(ObjectId id, Estoque model)
        {
            try
            {
                Expression<Func<Estoque, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Estoque>()
                    .Set(n => n.descricao, model.descricao)
                    .Set(n => n.preco, model.preco)
                    .Set(n => n.unidade_medida, model.unidade_medida)
                    .Set(n => n.quantidade_disponivel, model.quantidade_disponivel);

                var collection = connection.GetCollection();
                collection.FindOneAndUpdate(filter, update);

                return collection.Find(filter).FirstOrDefault();
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Estoque Delete(ObjectId id)
        {
            try
            {
                Expression<Func<Estoque, bool>> filter = x => x.id.Equals(id);

                return connection.GetCollection().FindOneAndDelete(filter);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
