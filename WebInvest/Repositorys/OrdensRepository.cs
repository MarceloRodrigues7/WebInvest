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
    public class OrdensRepository : IOrdensRepository
    {
        private readonly string _connectionString;

        public OrdensRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public void PostOrdem(Ordem ordem)
        {
            var query = @"INSERT INTO OrdensInvest(IdUsuario,IdAcao,Quantidade,ValorUn,ValorTotal,DataHora,StatusOrdem,Tipo)
                              VALUES(@IdUsuario,@IdAcao,@Quantidade,@ValorUn,@ValorTotal,@DataHora,@StatusOrdem,@Tipo)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new
                {
                    ordem.IdUsuario,
                    ordem.IdAcao,
                    ordem.Quantidade,
                    ordem.ValorUn,
                    ordem.ValorTotal,
                    ordem.DataHora,
                    ordem.StatusOrdem,
                    ordem.Tipo
                });
                connection.Close();
            };
        }
    }
}
