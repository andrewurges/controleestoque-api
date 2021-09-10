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
    public class EstoqueService : IControleEstoqueService<Estoque>
    {
        IControleEstoqueRepository<Estoque> _estoqueRepository;

        public EstoqueService(IControleEstoqueSettings settings)
        {
            _estoqueRepository = new EstoqueRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Estoque Get(ObjectId id)
        {
            return _estoqueRepository.Get(id);
        }

        public List<Estoque> GetAll(Expression<Func<Estoque, bool>> where = null)
        {
            return _estoqueRepository.GetAll(where);
        }

        public bool Create(Estoque model)
        {
            return _estoqueRepository.Create(model);
        }

        public bool Update(ObjectId id, Estoque model)
        {
            return _estoqueRepository.Update(id, model);
        }

        public bool Delete(ObjectId id)
        {
            return _estoqueRepository.Delete(id);
        }
    }
}
