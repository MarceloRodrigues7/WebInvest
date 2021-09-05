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
            var data = GetHistoricoAcao(baseAcao);
            return View(data);
        }

        [Authorize]
        public IActionResult Historico(BaseAcao baseAcao)
        {
            var data = GetHistoricoAcao(baseAcao);
            return View(data);
        }

        [Authorize]
        public IActionResult Negociar(BaseAcao baseAcao)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM Acoes WITH(NOLOCK) WHERE Id=@Id";
                var data = connection.QueryFirst<BaseAcao>(query, new { baseAcao.Id });
                var transferencia = new Transferencia
                {
                    _BaseAcao = data,
                    Quantidade = 0
                };
                return View(transferencia);
            };
        }

        [Authorize]
        public IActionResult Comprar(Transferencia transferencia)
        {
            if (transferencia._BaseAcao.ValorAtual < 0)
            {
                transferencia._BaseAcao.ValorAtual = transferencia._BaseAcao.ValorAtual * -1;
            }
            var saldo = GetSaldoUsuario();
            var valorTotal = transferencia.Quantidade * transferencia._BaseAcao.ValorAtual;
            try
            {
                if (saldo > valorTotal)
                {
                    var ordem = new Ordem()
                        .NovaOrdem(int.Parse(User.Identity.Name), transferencia._BaseAcao.Id, transferencia.Quantidade, transferencia._BaseAcao.ValorAtual, valorTotal, DateTime.Now, "Sucesso", true);
                    PostOrdem(ordem);
                    if (!ValidationInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name)))
                    {
                        PostInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name));
                    }
                    var quantidadeAtual = GetQuantidadeAcao(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name));
                    PutInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name), quantidadeAtual + transferencia.Quantidade);
                    PutSaldoUsuario(int.Parse(User.Identity.Name), saldo - valorTotal);
                    return RedirectToAction("Index", "Investimento");
                }
                TempData["Message"] = "Saldo insuficiente para realizar Compra!";
                return View("Negociar", transferencia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Message"] = "Ocorreu algum erro, tente novamente! " + ex.Message;
                return View("Negociar", transferencia);
            }
        }

        [Authorize]
        public IActionResult Vender(Transferencia transferencia)
        {
            var saldo = GetSaldoUsuario();
            var valorTotal = transferencia.Quantidade * transferencia._BaseAcao.ValorAtual;
            try
            {
                if (ValidationInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name)))
                {
                    var quantidadeAtual = GetQuantidadeAcao(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name));
                    if (transferencia.Quantidade <= quantidadeAtual)
                    {
                        var ordem = new Ordem().NovaOrdem(int.Parse(User.Identity.Name), transferencia._BaseAcao.Id, transferencia.Quantidade, transferencia._BaseAcao.ValorAtual, valorTotal, DateTime.Now, "Sucesso", false);
                        PostOrdem(ordem);
                        PutInvestimentoUsuario(transferencia._BaseAcao.Id, int.Parse(User.Identity.Name), quantidadeAtual - transferencia.Quantidade);
                        PutSaldoUsuario(int.Parse(User.Identity.Name), saldo + valorTotal);
                        return RedirectToAction("Index", "Investimento");
                    }
                }
                TempData["Message"] = "Quantidade insuficiente para realizar venda!";
                return View("Negociar", transferencia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Message"] = "Ocorreu algum erro, tente novamente! " + ex.Message;
                return View("Negociar", transferencia);
            }
        }

        private decimal GetSaldoUsuario()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Saldo FROM Usuarios WITH(NOLOCK) WHERE Id=@Id";
                var data = connection.QueryFirst<decimal>(query, new { Id = User.Identity.Name });
                return data;
            };
        }

        private int GetQuantidadeAcao(long idAcao, int idUsuario)
        {
            var query = "SELECT Quantidade FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res;
            };
        }

        private void PostOrdem(Ordem ordem)
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

        private bool ValidationInvestimentoUsuario(long idAcao, int idUsuario)
        {
            var query = "SELECT Count(*) FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res > 0;
            };
        }

        private void PostInvestimentoUsuario(long idAcao, int idUsuario)
        {
            var query = @"INSERT INTO InvestimentosUsuarios(IdUsuario,IdAcao,Quantidade)
                          VALUES(@idUsuario,@idAcao,0)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { idAcao, idUsuario });
                connection.Close();
            };
        }

        private void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade)
        {
            var query = "UPDATE InvestimentosUsuarios SET Quantidade=@quantidade WHERE IdAcao=@idAcao AND idUsuario=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { quantidade, idAcao, idUsuario });
                connection.Close();
            };
        }

        private void PutSaldoUsuario(long idUsuario, decimal novoSaldo)
        {
            var query = "UPDATE Usuarios SET Saldo=@novoSaldo WHERE Id=@idUsuario";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(query, new { novoSaldo, idUsuario });
                connection.Close();
            };
        }

        private IEnumerable<HistoricoAcao> GetHistoricoAcao(BaseAcao baseAcao)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Acoes.Id,Acoes.sigla,Acoes.acao AS 'Nome',Acoes.ValorAtual,HistoricoPrecoAcoes.DataHora,HistoricoPrecoAcoes.Valor FROM HistoricoPrecoAcoes 
                              LEFT JOIN Acoes ON(HistoricoPrecoAcoes.IdAcao= Acoes.Id) WHERE IdAcao=@Id and DataHora>=getdate()-30 order by DataHora asc";
                return connection.Query<HistoricoAcao>(query, new { baseAcao.Id });
            };
        }
    }
}
