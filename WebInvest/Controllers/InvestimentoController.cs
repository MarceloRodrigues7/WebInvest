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
    public class InvestimentoController : Controller
    {
        private readonly ILogger<InvestimentoController> _logger;
        private readonly string _connectionString;

        public InvestimentoController(ILogger<InvestimentoController> logger, IConfiguration configuration)
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
                var query = @"SELECT Acoes.Id,Acoes.sigla,InvestimentosUsuarios.Quantidade,Acoes.valorAtual,
                              Acoes.dataAtualizacao,InvestimentosUsuarios.Quantidade*Acoes.valorAtual AS 'AoVender'
                              FROM InvestimentosUsuarios INNER JOIN Acoes ON(InvestimentosUsuarios.IdAcao=Acoes.Id)
                              WHERE InvestimentosUsuarios.IdUsuario=@idUsuario";
                var data = connection.Query<InvestimentoUsuario>(query, new { idUsuario = User.Identity.Name });
                connection.Close();
                return View(data);
            };
        }
    }
}
