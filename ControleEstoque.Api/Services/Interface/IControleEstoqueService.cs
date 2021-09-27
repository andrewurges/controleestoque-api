using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleEstoque.Api.Interface
{
    public interface IControleEstoqueService<T> where T : class
    {
        T Get(ObjectId id);
        List<T> GetAll(Expression<Func<T, bool>> where = null);
        T Create(T model);
        T Update(ObjectId id, T model);
        T Delete(ObjectId id);
    }
}
