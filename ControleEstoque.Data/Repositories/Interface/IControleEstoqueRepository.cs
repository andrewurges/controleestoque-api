using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleEstoque.Data.Repositories.Interface
{
    public interface IControleEstoqueRepository<T> where T : class
    {
        T Get(ObjectId id);
        List<T> GetAll(Expression<Func<T, bool>> where = null);
        bool Create(T model);
        bool Update(ObjectId id, T model);
        bool Delete(ObjectId id);
    }
}
