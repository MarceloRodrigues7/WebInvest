using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInvest.Models
{
    public class Ordem
    {
        public long Id { get; set; }
        public int IdUsuario { get; set; }
        public long IdAcao { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUn { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataHora { get; set; }
        public string StatusOrdem { get; set; }
        public bool Tipo { get; set; }
    }
}
