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

        public Produto GetProduto(long idProduto)
        {
            var context = new EntityDb();
            try
            {
                return context.Produtos.Include(c => c.Tipo).Where(p=>p.Id==idProduto).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetValorProduto(long idProduto)
        {
            var context = new EntityDb();
            return context.Produtos.Where(p => p.Id == idProduto).First().ValorAtual;
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
