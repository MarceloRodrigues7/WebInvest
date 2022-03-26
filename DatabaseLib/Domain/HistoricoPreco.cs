using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class HistoricoPreco
    {
        public long Id { get; set; }
        public long ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }

        public Produto Produto { get; set; }        
    }
}
