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
        T Create(T model);
        T Update(ObjectId id, T model);
        T Delete(ObjectId id);
    }
}
