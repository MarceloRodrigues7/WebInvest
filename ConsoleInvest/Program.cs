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
        private static readonly string _connectionString = "server=svaz.database.windows.net;database=dbAzure;user=adm;password=P@ssword;Connection Timeout=1200";
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
            catch (Exception e )
            {
                Log.Error("Erro ao consultar valor acao. " + e.Message);
                return new decimal();
            }
        }

        private static decimal VariacaoValor(decimal valor)
        {
            var rand = new Random();
            var variacao = decimal.Parse(rand.NextDouble().ToString());
            if (rand.Next(-1, 2) <= 0)
            {
                return valor - variacao;
            }
            return valor + variacao;
        }

        private static void PostHistorico(long id, decimal valor, DateTime dataHora)
        {
            var query = "INSERT INTO HistoricoPrecoAcoes(IdAcao,Valor,DataHora)VALUES(@id,@valor,@dataHora)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { id, valor, dataHora });
                connection.Close();
            };
        }

        private static void AtualizaAcao(long id, decimal valor, DateTime dataAtualizacao)
        {
            var query = "UPDATE Acoes SET valorAtual=@valor, dataAtualizacao=@dataAtualizacao WHERE Id=@id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { valor, dataAtualizacao, id });
                connection.Close();
            };
        }
    }
}
