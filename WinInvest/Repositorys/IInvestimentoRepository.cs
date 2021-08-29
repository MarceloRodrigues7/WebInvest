using System.Collections.Generic;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public interface IInvestimentoRepository
    {
        IEnumerable<OrdemInvestimento> GetTransferencias(int IdUsuario);
        IEnumerable<InvestimentoUsuario> GetInvestimentos(int IdUsuario);
    }
}
