using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Repositorys
{
    public class AcoesRepository : IAcoesRepository
    {
        private readonly string _connectionString;
        public AcoesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public IEnumerable<BaseAcao> GetAcoes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM acoes";
                return connection.Query<BaseAcao>(query);

            };
        }

        public BaseAcao GetAcao(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT TOP(1) * FROM acoes WITH(NOLOCK) WHERE Id=@id";
                return connection.QueryFirstOrDefault<BaseAcao>(query, new { id });
            };
        }

        public IEnumerable<HistoricoAcao> GetHistoricoAcao(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Acoes.Id,Acoes.sigla,Acoes.acao AS 'Nome',Acoes.ValorAtual,HistoricoPrecoAcoes.DataHora,HistoricoPrecoAcoes.Valor FROM HistoricoPrecoAcoes 
                              LEFT JOIN Acoes ON(HistoricoPrecoAcoes.IdAcao= Acoes.Id) WHERE IdAcao=@id and DataHora>=getdate()-1 order by DataHora asc";
                return connection.Query<HistoricoAcao>(query, new { id });
            };
        }
    }
}
