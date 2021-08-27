using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public interface IOrdemRepository
    {
        bool Compra(Usuario usuario, BaseAcao acao, decimal valorTotal, int quantidade);
        bool Venda(Usuario usuario, BaseAcao acao, decimal valorTotal, int quantidade);
    }
}
