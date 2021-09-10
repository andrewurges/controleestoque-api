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
    public class ToDoService : IControleEstoqueService<ToDo>
    {
        IControleEstoqueRepository<ToDo> _receitaRepository;

        public ToDoService(IControleEstoqueSettings settings)
        {
            _receitaRepository = new ToDoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public ToDo Get(ObjectId id)
        {
            return _receitaRepository.Get(id);
        }

        public List<ToDo> GetAll(Expression<Func<ToDo, bool>> where = null)
        {
            return _receitaRepository.GetAll(where);
        }

        public bool Create(ToDo model)
        {
            return _receitaRepository.Create(model);
        }

        public bool Update(ObjectId id, ToDo model)
        {
            return _receitaRepository.Update(id, model);
        }

        public bool Delete(ObjectId id)
        {
            return _receitaRepository.Delete(id);
        }
    }
}
