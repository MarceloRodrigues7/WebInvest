using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Repositorys
{
    public interface IOrdensRepository
    {
        void PostOrdem(Ordem ordem);
    }
}
