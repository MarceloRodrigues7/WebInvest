using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Repositorys
{
    public interface IAcoesRepository
    {
        IEnumerable<BaseAcao> GetAcoes();
        BaseAcao GetAcao(long id);
        IEnumerable<HistoricoAcao> GetHistoricoAcao(long id);
    }
}
