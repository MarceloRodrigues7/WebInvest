using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Models
{
    public class BaseDashboard
    {
        public decimal? SaldoAtual { get; set; }
        public decimal? SaldoInvestido { get; set; }
        public int? Compras { get; set; }
        public int? Vendas { get; set; }
    }

    public class AcaoQuantidade
    {
        public int? IdAcao { get; set; }
        public int? Quantidade { get; set; }
    }

    public class RankSaldoAtual
    {
        public string Username { get; set; }
        public decimal Saldo { get; set; }
    }
}
