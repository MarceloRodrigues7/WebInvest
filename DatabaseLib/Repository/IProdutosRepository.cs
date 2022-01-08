using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IProdutosRepository
    {
        IEnumerable<Produto> GetProdutos();
        decimal GetValorProduto(long idProduto);
        void Post(Produto produto);
        void Put(Produto produto);
    }
}
