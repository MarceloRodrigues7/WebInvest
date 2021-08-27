using System.Collections.Generic;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public interface IInvestimentoRepository
    {
        IEnumerable<OrdemInvestimento> GetInvestimentos(int IdUsuario);
    }
}
