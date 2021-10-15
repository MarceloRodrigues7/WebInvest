using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Repositorys
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly string _connectionString;

        public UsuariosRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public decimal GetSaldoUsuario(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Saldo FROM Usuarios WITH(NOLOCK) WHERE Id=@id";
                var data = connection.QueryFirst<decimal>(query, new { id });
                return data;
            };
        }

        public void PutSaldoUsuario(long idUsuario, decimal novoSaldo)
        {
            var query = "UPDATE Usuarios SET Saldo=@novoSaldo WHERE Id=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { novoSaldo, idUsuario });
                connection.Close();
            };
        }
    }
}
