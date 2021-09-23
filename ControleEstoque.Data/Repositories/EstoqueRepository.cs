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
            catch (MongoConnectionException e)
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
            catch (MongoConnectionException e)
            {
                return new List<Estoque>();
            }
        }

        public bool Create(Estoque model)
        {
            var status = false;
            try
            {
                connection.GetCollection().InsertOne(model);
                status = true;
            }
            catch (MongoCommandException e)
            {
                status = false;
            }
            return status;
        }

        public bool Update(ObjectId id, Estoque model)
        {
            var status = false;
            try
            {
                Expression<Func<Estoque, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Estoque>()
                    .Set(n => n.descricao, model.descricao)
                    .Set(n => n.foto, model.foto)
                    .Set(n => n.preco, model.preco)
                    .Set(n => n.unidade_medida, model.unidade_medida)
                    .Set(n => n.quantidade_disponivel, model.quantidade_disponivel);
                    
                if (connection.GetCollection().FindOneAndUpdate(filter, update) != null)
                {
                    status = true;
                }
            }
            catch (MongoCommandException e)
            {
                throw e;
            }

            return status;
        }

        public bool Delete(ObjectId id)
        {
            var status = false;
            try
            {
                Expression<Func<Estoque, bool>> filter = x => x.id.Equals(id);

                if (connection.GetCollection().FindOneAndDelete(filter) != null)
                {
                    status = true;
                }
            }
            catch (MongoCommandException e)
            {
                throw e;
            }

            return status;
        }
    }
}
