using Dapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleInvest
{
    class Program
    {
        private static readonly string _connectionString = "Data Source=azsv.database.windows.net;Initial Catalog=azWebInvest;User ID=adm;Password=P@ssword;Connect Timeout=1000;";
        public static void Main()
        {
            Console.Title = "Worker Invest - version 1.0.0";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Usando Serilog...");
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log/.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();
            Log.Information("Iniciando worker");
            while (true)
            {
                List<Task> task = new();
                var listAcoes = GetAcoes();
                listAcoes.ForEach((acao) =>
                {
                    task.Add(Task.Factory.StartNew(() =>
                    {
                        var valorAtual = GetValorAcao(acao);
                        var novoValor = VariacaoValor(valorAtual);
                        var dataAtual = DateTime.Now.AddHours(-3);
                        PostHistorico(acao, novoValor, dataAtual);
                        AtualizaAcao(acao, novoValor, dataAtual);
                        Log.Information($"Id Ação: {acao} - Concluido com sucesso");
                    }));
                });
                Task.WaitAll(task.ToArray());
                task.Clear();
                listAcoes.Clear();
                Log.Information("Worker finalizado");
                Thread.Sleep(180000);
            }
        }

        private static List<long> GetAcoes()
        {
            var query = "SELECT Id FROM Acoes";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
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

        private static decimal GetValorAcao(long id)
        {
            var query = "SELECT valorAtual FROM Acoes WITH(NOLOCK) WHERE Id=@id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var res = connection.QueryFirst<decimal>(query, new { id });
                    connection.Close();
                    return res;
                };
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
            var interio = rand.Next(0, 1);
            var decimo = rand.Next(0, 99);
            decimal variacao = decimal.Parse($"{interio}.{decimo}");
            if (valor < 0)
            {
                if (rand.Next(0, 3) <= 0)
                {   
                    return valor - variacao;
                }
                return valor + variacao;
            }
            if (rand.Next(0, 2) <= 0)
            {
                return valor - variacao;
            }
            return valor + variacao;
        }

        private static void PostHistorico(long id, decimal valor, DateTime dataHora)
        {
            try
            {
                var query = "INSERT INTO HistoricoPrecoAcoes(IdAcao,Valor,DataHora)VALUES(@id,@valor,@dataHora)";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute(query, new { id, valor, dataHora });
                    connection.Close();
                };
            }
            catch (Exception e)
            {
                Log.Error($"Erro ao postar historico acao.id {id}. " + e.Message);
            }
        }

        private static void AtualizaAcao(long id, decimal valor, DateTime dataAtualizacao)
        {
            try
            {
                var query = "UPDATE Acoes SET valorAtual=@valor, dataAtualizacao=@dataAtualizacao WHERE Id=@id";
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute(query, new { valor, dataAtualizacao, id });
                    connection.Close();
                };
            }
            catch (Exception e)
            {
                Log.Error($"Erro ao atualizar acao.id {id}. " + e.Message);
            }
        }
    }
}
