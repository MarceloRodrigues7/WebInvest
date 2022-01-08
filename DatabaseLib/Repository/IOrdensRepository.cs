using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IOrdensRepository
    {
        IEnumerable<Ordem> GetOrdensStatusEnviado();
        void Put(Ordem ordem);
        void PostOrdem(Ordem ordem);
    }
}
