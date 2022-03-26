using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class Produto
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public long TipoId { get; set; }

        public List<HistoricoPreco> HistoricoPrecos { get; set; }
        public TipoProduto Tipo { get; set; }
        public List<InvestimentoUsuario> InvestimentosUsuario { get; set; }
    }
}
