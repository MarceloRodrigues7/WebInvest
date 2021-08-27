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
        public IEnumerable<OrdemInvestimento> GetInvestimentos(int IdUsuario)
        {
            using (var connection = new SqlConnection(Services.ConnectionString))
            {
                var query = @"SELECT Acoes.sigla,OrdensInvest.quantidade,OrdensInvest.valorUn,OrdensInvest.valorTotal,OrdensInvest.DataHora,OrdensInvest.StatusOrdem,IIF(OrdensInvest.Tipo=1,'Compra','Venda') AS 'Tipo'
                              FROM OrdensInvest INNER JOIN Acoes ON(OrdensInvest.IdAcao=Acoes.Id)
                              WHERE OrdensInvest.IdUsuario=@IdUsuario ORDER BY OrdensInvest.DataHora DESC";
                var data = connection.Query<OrdemInvestimento>(query, new { IdUsuario });
                return data;
            };
        }
    }
}
