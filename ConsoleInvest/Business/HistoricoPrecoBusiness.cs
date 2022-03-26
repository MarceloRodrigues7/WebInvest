using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Business
{
    public class HistoricoPrecoBusiness
    {
        public static HistoricoPreco GeraObj(Produto produto)
        {
            return new HistoricoPreco
            {
                DataHora = DateTime.UtcNow.AddHours(-3),
                ProdutoId = produto.Id,
                Valor = ProdutoBusiness.VariacaoValor(produto),
            };
        }
    }
}
