using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Controllers
{
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly string _connectionString;

        public TransacaoController(ILogger<TransacaoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        [Authorize]
        public IActionResult Index()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT Acoes.sigla,OrdensInvest.quantidade,OrdensInvest.valorUn,OrdensInvest.valorTotal,OrdensInvest.DataHora,OrdensInvest.StatusOrdem,IIF(OrdensInvest.Tipo=1,'Compra','Venda') AS 'Tipo'
                              FROM OrdensInvest INNER JOIN Acoes ON(OrdensInvest.IdAcao=Acoes.Id)
                              WHERE OrdensInvest.IdUsuario=@IdUsuario ORDER BY OrdensInvest.DataHora DESC";
                var data = connection.Query<OrdemInvestimento>(query, new { idUsuario = User.Identity.Name });
                connection.Close();
                return View(data);
            };
        }
    }
}
