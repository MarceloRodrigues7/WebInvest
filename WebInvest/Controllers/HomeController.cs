using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Usuario_Comum"))
            {
                var data = GetDashBoard();
                return View(data);
            }
            return View();
        }

        public IActionResult Informacoes()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<RankSaldoAtual> GetDashBoard()
        {
            var idUsuario = User.Identity.Name;
            var saldo = GetSaldoUsuario(idUsuario);
            var ordens = GetOrdensInvest(idUsuario);
            var investido = GetSaldoInvestidoUsuario(idUsuario);
            var rankSaldoAtual = GetRankSaldo();
            ViewBag.BaseDashboard = new BaseDashboard
            {
                SaldoAtual = saldo,
                SaldoInvestido = investido,
                Vendas = ordens.Vendas,
                Compras = ordens.Compras
            };
            return rankSaldoAtual;
        }

        private decimal GetSaldoUsuario(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Saldo FROM Usuarios AS u WITH(NOLOCK) WHERE Id=@id";
                var data = connection.QueryFirstOrDefault<decimal>(query, new { id });
                connection.Close();
                return data;
            };
        }

        private BaseDashboard GetOrdensInvest(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"SELECT TOP (1) (SELECT COUNT(*) FROM OrdensInvest WITH(NOLOCK) WHERE Tipo=0 AND IdUsuario=@id) AS Vendas, 
                              (SELECT COUNT(*) FROM OrdensInvest WITH(NOLOCK) WHERE Tipo=1 AND IdUsuario=@id) AS Compras
                              FROM OrdensInvest WITH(NOLOCK) WHERE IdUsuario=@id";
                var data = connection.QueryFirstOrDefault<BaseDashboard>(query, new { id });
                connection.Close();
                if (data == null)
                {
                    return new BaseDashboard { Compras = 0, Vendas = 0 };
                }
                return data;
            };
        }

        private decimal GetSaldoInvestidoUsuario(string id)
        {
            decimal valorInvestido = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT IdAcao,Quantidade FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdUsuario=@id";
                var data = connection.Query<AcaoQuantidade>(query, new { id }).ToList();
                connection.Close();
                foreach (var acao in data)
                {
                    var valorAcao = GetValorAcao(acao.IdAcao.ToString());
                    valorInvestido += valorAcao * acao.Quantidade.Value;
                }
                return valorInvestido;
            };
        }

        private decimal GetValorAcao(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT ValorAtual FROM Acoes WITH(NOLOCK) WHERE Id=@id";
                var data = connection.QueryFirstOrDefault<decimal>(query, new { id });
                connection.Close();
                return data;
            };
        }

        private IEnumerable<RankSaldoAtual> GetRankSaldo()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Username,Saldo FROM Usuarios ORDER BY Saldo DESC";
                var data = connection.Query<RankSaldoAtual>(query);
                connection.Close();
                return data;
            };
        }
    }
}
