using DatabaseLib.Context;
using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class OrdensRepository : IOrdensRepository
    {
        public IEnumerable<Ordem> GetOrdensStatusEnviado()
        {
            var context = new EntityDb();
            return context.Ordens.Where(o => o.StatusOrdem == "Enviado");
        }

        public int GetTotalOrdensCompra(long idUsuario)
        {
            var context = new EntityDb();
            return context.Ordens.Where(o => o.IdUsuario == idUsuario && o.Tipo == true).Count();
        }

        public int GetTotalOrdensVenda(long idUsuario)
        {
            var context = new EntityDb();
            return context.Ordens.Where(o => o.IdUsuario == idUsuario && o.Tipo == false).Count();
        }

        public void PostOrdem(Ordem ordem)
        {
            var context = new EntityDb();
            context.Add(ordem);
            context.SaveChanges();
        }

        public void Put(Ordem ordem)
        {
            var context = new EntityDb();
            context.Update(ordem);
            context.SaveChanges();
        }
    }
}
