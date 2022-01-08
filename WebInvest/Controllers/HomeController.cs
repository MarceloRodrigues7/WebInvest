using Dapper;
using DatabaseLib.Domain;
using DatabaseLib.Repository;
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
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IOrdensRepository _ordensRepository;
        private readonly ILevelUsuariosRepository _levelUsuariosRepository;
        private readonly IInvestimentosUsuarioRepository _investimentosUsuarioRepository;
        private readonly IProdutosRepository _produtosRepository;

        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
            _usuariosRepository = new UsuariosRepository();
            _ordensRepository = new OrdensRepository();
            _levelUsuariosRepository = new LevelUsuariosRepository();
            _investimentosUsuarioRepository = new InvestimentosUsuarioRepository();
            _produtosRepository = new ProdutosRepository();
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

        public IActionResult Contact()
        {
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
            var idUsuario = long.Parse(User.Identity.Name);
            var saldo = _usuariosRepository.GetSaldoUsuario(idUsuario);
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
            ViewBag.LevelUsuario = _levelUsuariosRepository.GetLevelECategoriaUsuario(idUsuario);
            return rankSaldoAtual;
        }

        private BaseDashboard GetOrdensInvest(long idUsuario)
        {
            return new BaseDashboard
            {
                Compras = _ordensRepository.GetTotalOrdensCompra(idUsuario),
                Vendas = _ordensRepository.GetTotalOrdensVenda(idUsuario)
            };
        }

        private decimal GetSaldoInvestidoUsuario(long idUsuario)
        {
            decimal valorInvestido = 0;
            var produtos = _investimentosUsuarioRepository.GetInvestimentoUsuarios(idUsuario);
            foreach (var produto in produtos)
            {
                var valorAcao = _produtosRepository.GetValorProduto(produto.Id);
                valorInvestido += valorAcao * produto.Quantidade;
            }
            return valorInvestido;
        }

        private IEnumerable<RankSaldoAtual> GetRankSaldo()
        {
            var list = new List<RankSaldoAtual>();
            var res = _levelUsuariosRepository.GetLevelECategoriaUsuarios();
            foreach (var r in res)
            {
                list.Add(new RankSaldoAtual
                {
                    ExpAtual = r.ExpAtual,
                    Categoria = r.CategoriaLevel.Nome,
                    Username = r.Usuario.Username
                });
            }
            return list;
        }
    }
}
