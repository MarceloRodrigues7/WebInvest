using DatabaseLib.Context;
using DatabaseLib.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class ProdutosRepository: IProdutosRepository
    {
        public IEnumerable<Produto> GetProdutos()
        {
            var context = new EntityDb();
            try
            {
                return context.Produtos.Include(c=>c.Tipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Post(Produto produto)
        {
            using (var context = new EntityDb())
            {
                try
                {
                    context.Produtos.Add(produto);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            };
        }

        public void Put(Produto produto)
        {
            using (var context = new EntityDb())
            {
                try
                {
                    context.Produtos.Update(produto);
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
