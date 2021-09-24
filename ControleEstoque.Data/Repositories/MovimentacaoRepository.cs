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
    public class MovimentacaoRepository : IControleEstoqueRepository<Movimentacao>
    {
        public static string collectionName = "Movimentacao";

        MongoConnection<Movimentacao> connection;

        public MovimentacaoRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Movimentacao>(connectionString, databaseName, collectionName);
        }

        public Movimentacao Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Movimentacao();
            }
        }

        public List<Movimentacao> GetAll(Expression<Func<Movimentacao, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Movimentacao>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Movimentacao>();
            }
        }

        public bool Create(Movimentacao model)
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

        public bool Update(ObjectId id, Movimentacao model)
        {
            var status = false;
            try
            {
                Expression<Func<Movimentacao, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Movimentacao>()
                    .Set(n => n.tipo, model.tipo)
                    .Set(n => n.itens, model.itens);

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
                Expression<Func<Movimentacao, bool>> filter = x => x.id.Equals(id);

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
