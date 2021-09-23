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
            catch (MongoConnectionException e)
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
            catch (MongoConnectionException e)
            {
                return new List<Pedido>();
            }
        }

        public bool Create(Pedido model)
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

        public bool Update(ObjectId id, Pedido model)
        {
            var status = false;
            try
            {
                Expression<Func<Pedido, bool>> filter = x => x.id.Equals(id);
                var update = new UpdateDefinitionBuilder<Pedido>()
                    .Set(n => n.nome_cliente, model.nome_cliente)
                    .Set(n => n.lista_produto, model.lista_produto)
                    .Set(n => n.data, model.data)
                    .Set(n => n.situacao_pedido, model.situacao_pedido)
                    .Set(n => n.situacao_pagamento, model.situacao_pagamento);

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
                Expression<Func<Pedido, bool>> filter = x => x.id.Equals(id);

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
