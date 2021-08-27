using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinInvest.Models
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

        public Ordem NovaOrdem(int idUsuario, long idAcao, int quantidade, decimal valorUn, decimal valorTotal, DateTime dataHora, string statusOrdem,bool tipo)
        {
            return new Ordem
            {
                IdUsuario = idUsuario,
                IdAcao = idAcao,
                Quantidade = quantidade,
                ValorUn = valorUn,
                ValorTotal = valorTotal,
                DataHora = dataHora,
                StatusOrdem = statusOrdem,
                Tipo=tipo
            };
        }
    }
}
