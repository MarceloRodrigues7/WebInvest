using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Controllers
{
    public class AcoesController : Controller
    {
        private readonly string _connectionString;

        public AcoesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        [Authorize]
        public IActionResult Index()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM acoes";
                var data = connection.Query<BaseAcao>(query);
                return View(data);
            };
        }

        [Authorize]
        public IActionResult Informacao(BaseAcao baseAcao)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT DataHora,Valor FROM HistoricoPrecoAcoes WHERE IdAcao=@Id and DataHora>=getdate()-1 order by DataHora asc";
                var data = connection.Query<HistoricoAcao>(query,new { baseAcao.Id });
                return View(data);
            };
        }

    }
}
