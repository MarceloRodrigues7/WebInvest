using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Repositorys
{
    public class InvestimentosRepository : IInvestimentosRepository
    {
        private readonly string _connectionString;
        public InvestimentosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public int GetQuantidadeAcaoUsuario(long idAcao, int idUsuario)
        {
            var query = "SELECT Quantidade FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res;
            };
        }

        public void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade)
        {
            var query = "UPDATE InvestimentosUsuarios SET Quantidade=@quantidade WHERE IdAcao=@idAcao AND idUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { quantidade, idAcao, idUsuario });
                connection.Close();
            };
        }

        public bool ValidacaoInvestimentoUsuario(long idAcao, int idUsuario)
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
    }
}
