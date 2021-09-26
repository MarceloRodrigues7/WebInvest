using ConsoleInvest.Models;
using ConsoleInvest.Utils;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobTransacoes : IJobTransacoes
    {
        private static string _connectionString = Services.ConnectionString;


        public void TarefaTransacoes()
        {
            var ordens = GetOrdensStatusEnviado();
            foreach (var ordem in ordens)
            {
                var saldoUsuario = GetSaldoUsuario(ordem.IdUsuario);
                if (saldoUsuario > ordem.ValorTotal)
                {
                    if (!ValidaAcaoUsuario(ordem.IdAcao, ordem.IdUsuario))
                    {
                        PostInvestimentoUsuario(ordem.IdAcao, ordem.IdUsuario);
                    }
                    var quantidadeAtual = GetQuantidadeAcao(ordem.IdAcao, ordem.IdUsuario);
                    PutInvestimentoUsuario(ordem.IdAcao, ordem.IdUsuario, quantidadeAtual + ordem.Quantidade);
                    PutSaldoUsuario(ordem.IdUsuario, saldoUsuario - ordem.ValorTotal);
                    AtualizaOrdem(ordem.Id, "Sucesso");
                }
                else
                {
                    AtualizaOrdem(ordem.Id, "Falha");
                }
                Log.Information($"Ordem[{ordem.Id}] atualizada");
            }
            Log.Information($"Tarefa Transacoes finalizada.");
        }

        private List<Ordem> GetOrdensStatusEnviado()
        {
            var query = "SELECT * FROM OrdensInvest WHERE StatusOrdem='Enviado'";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.Query<Ordem>(query).ToList();
                connection.Close();
                return res;
            };
        }
        private void PostInvestimentoUsuario(long idAcao, int idUsuario)
        {
            var query = @"INSERT INTO InvestimentosUsuarios(IdUsuario,IdAcao,Quantidade)
                          VALUES(@idUsuario,@idAcao,0)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { idAcao, idUsuario });
                connection.Close();
            };
        }
        private decimal GetSaldoUsuario(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Saldo FROM Usuarios WITH(NOLOCK) WHERE Id=@id";
                var data = connection.QueryFirst<decimal>(query, new { id });
                return data;
            };
        }
        private bool ValidaAcaoUsuario(long idAcao, int idUsuario)
        {
            var query = "SELECT Count(*) FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res > 0;
            };
        }
        private int GetQuantidadeAcao(long idAcao, int idUsuario)
        {
            var query = "SELECT Quantidade FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.QueryFirstOrDefault<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res;
            };
        }
        private void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade)
        {
            var query = "UPDATE InvestimentosUsuarios SET Quantidade=@quantidade WHERE IdAcao=@idAcao AND idUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { quantidade, idAcao, idUsuario });
                connection.Close();
            };
        }
        private void PutSaldoUsuario(long idUsuario, decimal novoSaldo)
        {
            var query = "UPDATE Usuarios SET Saldo=@novoSaldo WHERE Id=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { novoSaldo, idUsuario });
                connection.Close();
            };
        }
        private void AtualizaOrdem(long id, string status)
        {
            var query = @"UPDATE OrdensInvest SET StatusOrdem=@status WHERE Id=@id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { id, status });
                connection.Close();
            };
        }
    }
}
