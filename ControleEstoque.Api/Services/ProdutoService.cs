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

        public Produto Create(Produto model)
        {
            return _produtoRepository.Create(model);
        }

        public Produto Update(ObjectId id, Produto model)
        {
            return _produtoRepository.Update(id, model);
        }

        public Produto Delete(ObjectId id)
        {
            return _produtoRepository.Delete(id);
        }
    }
}
