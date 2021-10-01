using ControleEstoque.Api.Interface;
using ControleEstoque.Api.Model;
using ControleEstoque.Data.Enum;
using ControleEstoque.Data.Model;
using ControleEstoque.Data.Repositories;
using ControleEstoque.Data.Repositories.Interface;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ControleEstoque.Api.Services
{
    public class PedidoService : IControleEstoqueService<Pedido>
    {
        IControleEstoqueRepository<Pedido> _receitaRepository;

        public PedidoService(IControleEstoqueSettings settings)
        {
            _receitaRepository = new PedidoRepository(settings.MongoConnectionString, settings.MongoDatabaseName);
        }

        public Pedido Get(ObjectId id)
        {
            return _receitaRepository.Get(id);
        }

        public List<Pedido> GetAll(Expression<Func<Pedido, bool>> where = null)
        {
            return _receitaRepository.GetAll(where);
        }

        public Pedido Create(Pedido model)
        {
            model.DataCriacao = DateTime.Now.ToString("dd/MM/yyyy");
            model.DataAtualizacao = "";
            model.SituacaoPedido = ESituacaoPedido.AFazer;
            model.Historico.Add(new HistoricoPedido()
            {
                Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                SituacaoPedido = ESituacaoPedido.AFazer
            });

            return _receitaRepository.Create(model);
        }

        public Pedido Update(ObjectId id, Pedido model)
        {
            model.DataAtualizacao = DateTime.Now.ToString("dd/MM/yyyy");

            if (model.Historico.Count > 0)
            {
                var ultimoHistorico = model.Historico[^1];
                if (ultimoHistorico.SituacaoPedido != model.SituacaoPedido)
                {
                    model.Historico.Add(new HistoricoPedido()
                    {
                        Data = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                        SituacaoPedido = model.SituacaoPedido
                    });
                }
            }

            return _receitaRepository.Update(id, model);
        }

        public Pedido Delete(ObjectId id)
        {
            return _receitaRepository.Delete(id);
        }
    }
}
