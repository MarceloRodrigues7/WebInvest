using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinInvest.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool StatusAtivo { get; set; }

        public decimal AtualizaSaldo(int Id)
        {
            var query = "SELECT Saldo FROM Usuarios WITH(NOLOCK) WHERE Id=@Id";
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<decimal>(query, new { Id });
                connection.Close();
                return res;
            };
        }
    }
}
