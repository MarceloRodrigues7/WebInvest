using ConsoleInvest.Models;
using ConsoleInvest.Utils;
using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleInvest.Jobs
{
    public class JobAcoes : IJobAcoes
    {
        public bool TarefaAcoes()
        {
            List<Task> task = new();
            var acoes = GetAcoes();

            acoes.ForEach((acao) =>
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

        private static void AtualizaAcao(BAcao acao)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                try
                {
                    var novoValor = VariacaoValor(acao.ValorAtual);
                    var dataAtual = DateTime.UtcNow.AddHours(-3);
                    AtualizarDb(acao.Id, novoValor, dataAtual, connection);
                    Log.Information($"Ação[{acao.Sigla}] | Anterior[{acao.ValorAtual}] -> Novo[{novoValor}]");
                }
                catch (Exception e)
                {
                    Log.Error($"Erro ao atualizar acao[{acao.Sigla}] " + e.Message);
                }
                finally
                {
                    connection.Close();
                }
            };
        }

        private static List<BAcao> GetAcoes()
        {
            Log.Information("Consultado lista Ações");
            try
            {
                using (var connection = new SqlConnection(Services.ConnectionString))
                {
                    var query = "SELECT Id,Sigla,ValorAtual FROM Acoes";
                    connection.Open();
                    var res = connection.Query<BAcao>(query).ToList();
                    connection.Close();
                    return res;
                };
            }
            catch (Exception e)
            {
                Log.Error("Erro ao consultar Acoes.Id. " + e.Message);
                return new List<BAcao>();
            }
        }

        private static decimal VariacaoValor(decimal valor)
        {
            var rand = new Random();
            var sorteio = rand.Next(-2, 2);
            decimal variacao = GeraValor(rand);
            if (sorteio < 0)
            {
                return valor - variacao;
            }
            else if (sorteio > 0){
                return valor + variacao;
            }
            return valor;
        }

        private static decimal GeraValor(Random rand)
        {
            var culture = new CultureInfo("pt-BR");
            var interio = rand.Next(0, 1);
            var priDecimo = rand.Next(0, 9);
            var segDecimo = rand.Next(1, 9);
            decimal variacao = decimal.Parse($"{interio},{priDecimo}{segDecimo}", culture);
            return variacao;
        }

        private static void AtualizarDb(long id, decimal valor, DateTime dataHora, SqlConnection connection)
        {
            try
            {
                PostHistorico(id, valor, dataHora, connection);
                PutAcao(id, valor, dataHora, connection);
            }
            catch (Exception e)
            {
                Log.Error($"Erro interno - acao.id[{id}] -> " + e.Message);
            }
        }

        private static void PostHistorico(long id, decimal valor, DateTime dataHora, SqlConnection connection)
        {
            var query = "INSERT INTO HistoricoPrecoAcoes(IdAcao,Valor,DataHora)VALUES(@id,@valor,@dataHora)";
            connection.Execute(query, new { id, valor, dataHora });
        }

        private static void PutAcao(long id, decimal valor, DateTime dataHora, SqlConnection connection)
        {
            var query = "UPDATE Acoes SET valorAtual=@valor, dataAtualizacao=@dataHora WHERE Id=@id";
            connection.Execute(query, new { valor, dataHora, id });
        }

    }
}
