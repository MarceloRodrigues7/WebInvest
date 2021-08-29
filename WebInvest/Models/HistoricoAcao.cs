using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Models
{
    public class HistoricoAcao
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataHora { get; set; }
        public decimal Valor { get; set; }        
    }
}
