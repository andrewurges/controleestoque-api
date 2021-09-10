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
    public class ToDoRepository : IControleEstoqueRepository<ToDo>
    {
        public static string collectionName = "ToDo";

        MongoConnection<ToDo> connection;

        public ToDoRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<ToDo>(connectionString, databaseName, collectionName);
        }

        public ToDo Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException e)
            {
                return new ToDo();
            }
        }

        public List<ToDo> GetAll(Expression<Func<ToDo, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<ToDo>(where).ToList();
                }
            }
            catch (MongoConnectionException e)
            {
                return new List<ToDo>();
            }
        }

        public bool Create(ToDo model)
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

        public bool Update(ObjectId id, ToDo model)
        {
            var status = false;
            try
            {
                Expression<Func<ToDo, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<ToDo>().Set(n => n, model);

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
                Expression<Func<ToDo, bool>> filter = x => x.id.Equals(id);

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
