using DatabaseLib.Context;
using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class HistoricoPrecosRepository : IHistoricoPrecosRepository
    {
        public IEnumerable<HistoricoPreco> GetHistoricos()
        {
            using (var context = new EntityDb())
            {
                try
                {
                    return context.HistoricoPrecos;
                }
                catch (Exception)
                {
                    throw;
                }
            };
        }

        public void Post(HistoricoPreco historico)
        {
            using (var context = new EntityDb())
            {
                try
                {
                    context.HistoricoPrecos.Add(historico);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            };
        }

        public void Put(HistoricoPreco historico)
        {
            using (var context = new EntityDb())
            {
                try
                {
                    context.HistoricoPrecos.Update(historico);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            };
        }
    }
}
