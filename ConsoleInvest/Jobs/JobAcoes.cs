using ConsoleInvest.Utils;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobAcoes : IJobAcoes
    {
        public bool TarefaAcoes()
        {
            List<Task> task = new();
            GetAcoes().ForEach((acao) =>
            {
                task.Add(Task.Factory.StartNew(() =>
                {
                    AtualizaAcao(acao);
                }));
            });
            Task.WaitAll(task.ToArray());
            task.Clear();
            Log.Information($"Tarefa Ações finalizada.");
            return true;
        }

        private void AtualizaAcao(long acao)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                try
                {
                    var valorAtual = GetValorAcao(acao, connection);
                    var novoValor = VariacaoValor(valorAtual);
                    var dataAtual = DateTime.UtcNow.AddHours(-3);
                    AtualizarDb(acao, novoValor, dataAtual,connection);
                    Log.Information($"Ação[{acao}] V.Anterior[{valorAtual}] V.Novo[{novoValor}]");
                }
                catch (Exception e)
                {
                    Log.Error($"Erro ao atualizar acao[{acao}] " + e.Message);
                }
                finally
                {
                    connection.Close();
                }
            };
        }

        private static List<long> GetAcoes()
        {
            Log.Information("Consultado lista Ações");
            try
            {
                using (var connection = new SqlConnection(Services.ConnectionString))
                {
                    var query = "SELECT Id FROM Acoes";
                    connection.Open();
                    var res = connection.Query<long>(query).ToList();
                    connection.Close();
                    return res;
                };
            }
            catch (Exception e)
            {
                Log.Error("Erro ao consultar Acoes.Id. " + e.Message);
                return new List<long>();
            }
        }

        private static decimal GetValorAcao(long id, SqlConnection connection)
        {
            try
            {
                var query = "SELECT valorAtual FROM Acoes WITH(NOLOCK) WHERE Id=@id";
                var res = connection.QueryFirst<decimal>(query, new { id });
                return res;
            }
            catch (Exception e)
            {
                Log.Error("Erro ao consultar valor acao. " + e.Message);
                return new decimal();
            }
        }

        private static decimal VariacaoValor(decimal valor)
        {
            var rand = new Random();
            var sorteio = rand.Next(-1, 3);
            decimal variacao = GeraValor(rand);
            if (valor>0)
            {
                if (sorteio <= 0)
                {
                    return valor - variacao;
                }
            }
            return valor + variacao;
        }

        private static decimal GeraValor(Random rand)
        {
            var interio = rand.Next(0, 1);
            var priDecimo = rand.Next(0, 9);
            var segDecimo = rand.Next(1, 9);
            decimal variacao = decimal.Parse($"{interio},{priDecimo}{segDecimo}");
            return variacao;
        }

        private void AtualizarDb(long id, decimal valor, DateTime dataHora, SqlConnection connection)
        {
            try
            {
                PostHistorico(ref id, ref valor, ref dataHora, connection);
                PutAcao(ref id, ref valor, ref dataHora, connection);
            }
            catch (Exception e)
            {
                Log.Error($"Erro interno - acao.id[{id}] -> " + e.Message);
            }
        }

        private static void PostHistorico(ref long id, ref decimal valor, ref DateTime dataHora, SqlConnection connection)
        {
            var query = "INSERT INTO HistoricoPrecoAcoes(IdAcao,Valor,DataHora)VALUES(@id,@valor,@dataHora)";
            connection.Execute(query, new { id, valor, dataHora });
        }

        private static void PutAcao(ref long id, ref decimal valor, ref DateTime dataHora, SqlConnection connection)
        {
            var query = "UPDATE Acoes SET valorAtual=@valor, dataAtualizacao=@dataHora WHERE Id=@id";
            connection.Execute(query, new { valor, dataHora, id });
        }

    }
}
