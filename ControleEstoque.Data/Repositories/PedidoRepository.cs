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
    public class PedidoRepository : IControleEstoqueRepository<Pedido>
    {
        public static string collectionName = "Pedido";

        MongoConnection<Pedido> connection;

        public PedidoRepository(string connectionString, string databaseName)
        {
            connection = new MongoConnection<Pedido>(connectionString, databaseName, collectionName);
        }

        public Pedido Get(ObjectId id)
        {
            try
            {
                return connection.GetCollection().Find(x => x.id.Equals(id)).FirstOrDefault();
            }
            catch (MongoConnectionException)
            {
                return new Pedido();
            }
        }

        public List<Pedido> GetAll(Expression<Func<Pedido, bool>> where = null)
        {
            try
            {
                if (where == null)
                {
                    return connection.GetCollection().Find(new BsonDocument()).ToList();
                }
                else
                {
                    return connection.GetCollection().Find<Pedido>(where).ToList();
                }
            }
            catch (MongoConnectionException)
            {
                return new List<Pedido>();
            }
        }

        public Pedido Create(Pedido model)
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

        public Pedido Update(ObjectId id, Pedido model)
        {
            try
            {
                Expression<Func<Pedido, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Pedido>()
                    .Set(n => n.nome_cliente, model.nome_cliente)
                    .Set(n => n.lista_produto, model.lista_produto)
                    .Set(n => n.data, model.data)
                    .Set(n => n.situacao_pedido, model.situacao_pedido)
                    .Set(n => n.situacao_pagamento, model.situacao_pagamento);

                var collection = connection.GetCollection();
                collection.FindOneAndUpdate(filter, update);

                return collection.Find(filter).FirstOrDefault();
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }

        public Pedido Delete(ObjectId id)
        {
            try
            {
                Expression<Func<Pedido, bool>> filter = x => x.id.Equals(id);

                return connection.GetCollection().FindOneAndDelete(filter);
            }
            catch (MongoCommandException e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
