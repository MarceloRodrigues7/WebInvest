using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerInvest
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly string _connectionString;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var listAcoes = GetAcoes();
                foreach (var acao in listAcoes)
                {
                    _logger.LogInformation($"Id Ação: {acao}");
                    var valorAtual = GetValorAcao(acao);
                    var novoValor = VariacaoValor(valorAtual);
                    var dataAtual = DateTime.Now;
                    _logger.LogInformation($"Atualizando informações");
                    PostHistorico(acao, novoValor, dataAtual);
                    AtualizaAcao(acao, novoValor, dataAtual);
                    _logger.LogInformation($"Concluido com sucesso");
                    await Task.Delay(1000, stoppingToken);
                }
                await Task.Delay(300000, stoppingToken);
            }
        }

        private List<long> GetAcoes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id FROM Acoes";
                return connection.Query<long>(query).ToList();
            };
        }

        private decimal GetValorAcao(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT valorAtual FROM Acoes WITH(NOLOCK) WHERE Id=@id";
                return connection.QueryFirst<decimal>(query, new { id });
            };
        }

        private decimal VariacaoValor(decimal valor)
        {
            var rand = new Random();
            var variacao = decimal.Parse(rand.NextDouble().ToString());
            if (rand.Next(-2, 2) <= 0)
            {
                return valor - variacao;
            }
            return valor + variacao;
        }

        private void PostHistorico(long id, decimal valor, DateTime dataHora)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO HistoricoPrecoAcoes(IdAcao,Valor,DataHora)VALUES(@id,@valor,@dataHora)";
                connection.Execute(query, new { id, valor, dataHora });
            };
        }

        private void AtualizaAcao(long id, decimal valor, DateTime dataAtualizacao)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Acoes SET valorAtual=@valor, dataAtualizacao=@dataAtualizacao WHERE Id=@id";
                connection.Execute(query, new { valor, dataAtualizacao, id });
            };
        }
    }
}
