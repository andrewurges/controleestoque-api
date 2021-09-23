using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Data.Model;
using ControleEstoque.Data.Repositories;
using ControleEstoque.Data.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleEstoque.Api.Services
{
    public class PedidoService : IControleEstoqueService<Pedido>
    {
        IControleEstoqueRepository<Pedido> _receitaRepository;

        public PedidoService(IControleEstoqueSettings settings)
        {
            _receitaRepository = new PedidoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Pedido Get(ObjectId id)
        {
            return _receitaRepository.Get(id);
        }

        public List<Pedido> GetAll(Expression<Func<Pedido, bool>> where = null)
        {
            return _receitaRepository.GetAll(where);
        }

        public bool Create(Pedido model)
        {
            return _receitaRepository.Create(model);
        }

        public bool Update(ObjectId id, Pedido model)
        {
            return _receitaRepository.Update(id, model);
        }

        public bool Delete(ObjectId id)
        {
            return _receitaRepository.Delete(id);
        }
    }
}
