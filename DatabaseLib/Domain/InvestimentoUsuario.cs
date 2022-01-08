using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class InvestimentoUsuario
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public Usuario Usuario { get; set; }
        public Produto Produto { get; set; }
    }
}
