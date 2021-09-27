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
    public class ConsumoService : IControleEstoqueService<Consumo>
    {
        IControleEstoqueRepository<Consumo> _consumoRepository;

        public ConsumoService(IControleEstoqueSettings settings)
        {
            _consumoRepository = new ConsumoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Consumo Get(ObjectId id)
        {
            return _consumoRepository.Get(id);
        }

        public List<Consumo> GetAll(Expression<Func<Consumo, bool>> where = null)
        {
            return _consumoRepository.GetAll(where);
        }

        public Consumo Create(Consumo model)
        {
            return _consumoRepository.Create(model);
        }

        public Consumo Update(ObjectId id, Consumo model)
        {
            return _consumoRepository.Update(id, model);
        }

        public Consumo Delete(ObjectId id)
        {
            return _consumoRepository.Delete(id);
        }
    }
}
