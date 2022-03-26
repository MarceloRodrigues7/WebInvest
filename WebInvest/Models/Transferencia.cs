using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Models
{
    public class Transferencia
    {
        public Produto BaseProduto { get; set; }
        public int Quantidade { get; set; }
    }
}
