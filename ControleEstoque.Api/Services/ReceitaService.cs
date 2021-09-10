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
    public class ReceitaService : IControleEstoqueService<Receita>
    {
        IControleEstoqueRepository<Receita> _receitaRepository;

        public ReceitaService(IControleEstoqueSettings settings)
        {
            _receitaRepository = new ReceitaRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Receita Get(ObjectId id)
        {
            return _receitaRepository.Get(id);
        }

        public List<Receita> GetAll(Expression<Func<Receita, bool>> where = null)
        {
            return _receitaRepository.GetAll(where);
        }

        public bool Create(Receita model)
        {
            return _receitaRepository.Create(model);
        }

        public bool Update(ObjectId id, Receita model)
        {
            return _receitaRepository.Update(id, model);
        }

        public bool Delete(ObjectId id)
        {
            return _receitaRepository.Delete(id);
        }
    }
}
