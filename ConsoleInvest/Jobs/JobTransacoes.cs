using DatabaseLib.Domain;
using DatabaseLib.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobTransacoes
    {
        private readonly IOrdensRepository _ordensRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IInvestimentosUsuarioRepository _investimentosUsuarioRepository;

        public JobTransacoes()
        {
            _ordensRepository = new OrdensRepository();
            _usuariosRepository = new UsuariosRepository();
            _investimentosUsuarioRepository = new InvestimentosUsuarioRepository();
        }

        public void TarefaTransacoes()
        {
            var ordens = _ordensRepository.GetOrdensStatusEnviado();
            foreach (var ordem in ordens)
            {
                var saldoUsuario = _usuariosRepository.GetSaldoUsuario(ordem.IdUsuario);
                if (saldoUsuario > ordem.ValorTotal)
                {
                    if (!_investimentosUsuarioRepository.ValidaAcaoUsuario(ordem.IdAcao, ordem.IdUsuario))
                    {
                        _investimentosUsuarioRepository.PostInvestimentoUsuario(ordem.IdAcao, ordem.IdUsuario);
                    }
                    var quantidadeAtual = _investimentosUsuarioRepository.GetQuantidadeAcao(ordem.IdAcao, ordem.IdUsuario);
                    _investimentosUsuarioRepository.PutInvestimentoUsuario(ordem.IdAcao, ordem.IdUsuario, quantidadeAtual + ordem.Quantidade);
                    _usuariosRepository.PutSaldoUsuario(ordem.IdUsuario, saldoUsuario - ordem.ValorTotal);
                    AtualizaOrdem(ordem, "Sucesso");
                }
                else
                {
                    AtualizaOrdem(ordem, "Falha");
                }
                Log.Information($"Ordem[{ordem.Id}] atualizada");
            }
            Log.Information($"Tarefa Transacoes finalizada.");
        }

        private void AtualizaOrdem(Ordem ordem, string status)
        {
            ordem.StatusOrdem = status;
            _ordensRepository.Put(ordem);
        }
    }
}
