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
    public class ClienteService : IControleEstoqueService<Cliente>
    {
        IControleEstoqueRepository<Cliente> _clienteRepository;

        public ClienteService(IControleEstoqueSettings settings)
        {
            _clienteRepository = new ClienteRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Cliente Get(ObjectId id)
        {
            return _clienteRepository.Get(id);
        }

        public List<Cliente> GetAll(Expression<Func<Cliente, bool>> where = null)
        {
            return _clienteRepository.GetAll(where);
        }

        public Cliente Create(Cliente model)
        {
            return _clienteRepository.Create(model);
        }

        public Cliente Update(ObjectId id, Cliente model)
        {
            return _clienteRepository.Update(id, model);
        }

        public Cliente Delete(ObjectId id)
        {
            return _clienteRepository.Delete(id);
        }
    }
}
