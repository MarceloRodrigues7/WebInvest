using ConsoleInvest.Business;
using DatabaseLib.Domain;
using DatabaseLib.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobProdutos
    {
        private readonly IProdutosRepository _produtosRepository;
        private readonly IHistoricoPrecosRepository _historicoPrecosRepository;

        public JobProdutos()
        {
            _produtosRepository = new ProdutosRepository();
            _historicoPrecosRepository = new HistoricoPrecosRepository();
        }

        public bool TarefaProdutos()
        {
            List<Task> task = new();
            var produtos = _produtosRepository.GetProdutos();
            foreach (var produto in produtos)
            {
                task.Add(Task.Factory.StartNew(() =>
                {
                    Atualiza(produto);
                }));
            }
            Task.WaitAll(task.ToArray());
            task.Clear();
            Log.Information($"Tarefa Ações finalizada.");
            return true;
        }

        private void Atualiza(Produto produto)
        {
            var historico = HistoricoPrecoBusiness.GeraObj(produto);
            _historicoPrecosRepository.Post(historico);
            produto = ProdutoBusiness.AtualizaObj(produto, historico);
            _produtosRepository.Put(produto);
            Log.Information($"Ação[{produto.Sigla}] | Valor[{produto.ValorAtual}]");
        }

    }
}
