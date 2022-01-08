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
        int GetTotalOrdensVenda(long idUsuario);
        int GetTotalOrdensCompra(long idUsuario);
        IEnumerable<Ordem> GetOrdensStatusEnviado();
        void Put(Ordem ordem);
        void PostOrdem(Ordem ordem);
    }
}
