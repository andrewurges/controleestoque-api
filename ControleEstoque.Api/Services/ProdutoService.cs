using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Data.Interface;
using ControleEstoque.Data.Model;
using ControleEstoque.Data.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleEstoque.Api.Services
{
    public class ProdutoService : IControleEstoqueService<Produto>
    {
        IControleEstoqueRepository<Produto> _produtoRepository;

        public ProdutoService(IControleEstoqueSettings settings)
        {
            _produtoRepository = new ProdutoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Produto Get(ObjectId id)
        {
            return _produtoRepository.Get(id);
        }

        public List<Produto> GetAll(Expression<Func<Produto, bool>> where = null)
        {
            return _produtoRepository.GetAll(where);
        }

        public bool Create(Produto model)
        {
            return _produtoRepository.Create(model);
        }

        public bool Update(ObjectId id, Produto model)
        {
            return _produtoRepository.Update(id, model);
        }

        public bool Delete(ObjectId id)
        {
            return _produtoRepository.Delete(id);
        }
    }
}
