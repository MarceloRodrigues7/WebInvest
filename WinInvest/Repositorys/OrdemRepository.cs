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
    public class OrdemRepository : IOrdemRepository
    {
        public bool Compra(Usuario usuario, BaseAcao acao, decimal valorTotal, int quantidade)
        {
            try
            {
                if (usuario.Saldo > valorTotal)
                {
                    var ordem = new Ordem().NovaOrdem(usuario.Id, acao.Id, quantidade, acao.ValorAtual, valorTotal, DateTime.Now, "Sucesso", true);
                    PostOrdem(ordem);
                    if (!ValidationInvestimentoUsuario(acao.Id, usuario.Id))
                    {
                        PostInvestimentoUsuario(acao.Id, usuario.Id);
                    }
                    var quantidadeAtual = GetQuantidadeAcao(acao.Id, usuario.Id);
                    PutInvestimentoUsuario(acao.Id, usuario.Id, quantidadeAtual + quantidade);
                    PutSaldoUsuario(usuario.Id, usuario.Saldo - valorTotal);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool Venda(Usuario usuario, BaseAcao acao, decimal valorTotal, int quantidade)
        {
            try
            {
                if (ValidationInvestimentoUsuario(acao.Id, usuario.Id))
                {
                    var quantidadeAtual = GetQuantidadeAcao(acao.Id, usuario.Id);
                    if (quantidade <= quantidadeAtual)
                    {
                        var ordem = new Ordem().NovaOrdem(usuario.Id, acao.Id, quantidade, acao.ValorAtual, valorTotal, DateTime.Now, "Sucesso", false);
                        PostOrdem(ordem);
                        PutInvestimentoUsuario(acao.Id, usuario.Id, quantidadeAtual - quantidade);
                        PutSaldoUsuario(usuario.Id, usuario.Saldo + valorTotal);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void PostOrdem(Ordem ordem)
        {
            var query = @"INSERT INTO OrdensInvest(IdUsuario,IdAcao,Quantidade,ValorUn,ValorTotal,DataHora,StatusOrdem,Tipo)
                              VALUES(@IdUsuario,@IdAcao,@Quantidade,@ValorUn,@ValorTotal,@DataHora,@StatusOrdem,@Tipo)";
            using (var connection = new SqlConnection(Services.ConnectionString))
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

        private int GetQuantidadeAcao(long idAcao, int idUsuario)
        {
            var query = "SELECT Quantidade FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var res = connection.QueryFirst<int>(query, new { idAcao, idUsuario });
                connection.Close();
                return res;
            };
        }

        private bool ValidationInvestimentoUsuario(long idAcao, int idUsuario)
        {
            var query = "SELECT Count(*) FROM InvestimentosUsuarios WITH(NOLOCK) WHERE IdAcao=@idAcao AND IdUsuario=@idUsuario";
            using (var connection = new SqlConnection(Services.ConnectionString))
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
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                connection.Execute(query, new { idAcao, idUsuario });
                connection.Close();
            };
        }

        private void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade)
        {
            var query = "UPDATE InvestimentosUsuarios SET Quantidade=@quantidade WHERE IdAcao=@idAcao AND idUsuario=@idUsuario";
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                connection.Execute(query, new { quantidade, idAcao, idUsuario });
                connection.Close();
            };
        }

        private void PutSaldoUsuario(long idUsuario, decimal novoSaldo)
        {
            var query = "UPDATE Usuarios SET Saldo=@novoSaldo WHERE Id=@idUsuario";
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                connection.Execute(query, new { novoSaldo, idUsuario });
                connection.Close();
            };
        }
    }
}
