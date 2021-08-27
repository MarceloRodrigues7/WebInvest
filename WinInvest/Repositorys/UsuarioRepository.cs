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
    public class UsuarioRepository : IUsuarioRepository
    {
        public Usuario GetUsuario(string username, string password)
        {
            var query = "SELECT * FROM Usuarios WITH(NOLOCK) WHERE Username=@username AND Password=@password";
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var data = connection.QueryFirstOrDefault<Usuario>(query, new { username, password });
                connection.Close();
                return data;
            };
        }
    }
}
