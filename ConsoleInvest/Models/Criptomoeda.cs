using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Models
{
    public class Criptomoeda
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
