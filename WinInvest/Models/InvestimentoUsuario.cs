﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinInvest.Models
{
    public class InvestimentoUsuario
    {
        public long Id { get; set; }
        public string Sigla { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public decimal AoVender { get; set; }
    }
}
