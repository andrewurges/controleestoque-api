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
    public class MovimentacaoService : IControleEstoqueService<Movimentacao>
    {
        IControleEstoqueRepository<Movimentacao> _movimentacaoRepository;

        public MovimentacaoService(IControleEstoqueSettings settings)
        {
            _movimentacaoRepository = new MovimentacaoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Movimentacao Get(ObjectId id)
        {
            return _movimentacaoRepository.Get(id);
        }

        public List<Movimentacao> GetAll(Expression<Func<Movimentacao, bool>> where = null)
        {
            return _movimentacaoRepository.GetAll(where);
        }

        public Movimentacao Create(Movimentacao model)
        {
            return _movimentacaoRepository.Create(model);
        }

        public Movimentacao Update(ObjectId id, Movimentacao model)
        {
            return _movimentacaoRepository.Update(id, model);
        }

        public Movimentacao Delete(ObjectId id)
        {
            return _movimentacaoRepository.Delete(id);
        }
    }
}
