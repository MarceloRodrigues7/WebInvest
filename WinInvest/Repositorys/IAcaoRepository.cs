using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public interface IAcaoRepository
    {
        IEnumerable<BaseAcao> GetAcoes();
        BaseAcao GetAcao(string cod);
    }
}
