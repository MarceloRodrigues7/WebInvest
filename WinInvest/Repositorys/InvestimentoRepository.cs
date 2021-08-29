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
    public class InvestimentoRepository: IInvestimentoRepository
    {
        public IEnumerable<OrdemInvestimento> GetTransferencias(int IdUsuario)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var query = @"SELECT Acoes.sigla,OrdensInvest.quantidade,OrdensInvest.valorUn,OrdensInvest.valorTotal,OrdensInvest.DataHora,OrdensInvest.StatusOrdem,IIF(OrdensInvest.Tipo=1,'Compra','Venda') AS 'Tipo'
                              FROM OrdensInvest INNER JOIN Acoes ON(OrdensInvest.IdAcao=Acoes.Id)
                              WHERE OrdensInvest.IdUsuario=@IdUsuario ORDER BY OrdensInvest.DataHora DESC";
                var data = connection.Query<OrdemInvestimento>(query, new { IdUsuario });
                connection.Close();
                return data;
            };
        }

        public IEnumerable<InvestimentoUsuario> GetInvestimentos(int IdUsuario)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                connection.Open();
                var query = @"SELECT InvestimentosUsuarios.Id,Acoes.sigla,InvestimentosUsuarios.Quantidade,Acoes.valorAtual,
                              Acoes.dataAtualizacao,InvestimentosUsuarios.Quantidade*Acoes.valorAtual AS 'AoVender'
                              FROM InvestimentosUsuarios INNER JOIN Acoes ON(InvestimentosUsuarios.IdAcao=Acoes.Id)
                              WHERE InvestimentosUsuarios.IdUsuario=@idUsuario";
                var data = connection.Query<InvestimentoUsuario>(query, new { IdUsuario });
                connection.Close();
                return data;
            };
        }
    }
}
