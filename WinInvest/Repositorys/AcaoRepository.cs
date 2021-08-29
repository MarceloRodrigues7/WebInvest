using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public class AcaoRepository :IAcaoRepository
    {
        public IEnumerable<BaseAcao> GetAcoes()
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                var query = "SELECT * FROM acoes ORDER BY Sigla ASC";
                var data = connection.Query<BaseAcao>(query);
                return data;
            };
        }

        public BaseAcao GetAcao(string cod)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                var query = "SELECT * FROM acoes WITH(NOLOCK) WHERE Id=@cod";
                var data = connection.QueryFirst<BaseAcao>(query, new { cod });
                return data;
            };
        }

        public IEnumerable<string> GetAcoesSiglas()
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                var query = "SELECT sigla FROM acoes ORDER BY sigla ASC";
                var data = connection.Query<string>(query);
                return data;
            };
        }

        public IEnumerable<string> GetAcoesNomes()
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                var query = "SELECT acao FROM acoes ORDER BY acao ASC";
                var data = connection.Query<string>(query);
                return data;
            };
        }
    }
}
