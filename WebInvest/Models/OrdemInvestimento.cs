using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Models
{
    public class OrdemInvestimento
    {
        public string Sigla { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUn { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataHora { get; set; }
        public string StatusOrdem { get; set; }
        public string Tipo { get; set; }
    }
}
