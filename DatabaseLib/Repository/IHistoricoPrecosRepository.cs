﻿using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IHistoricoPrecosRepository
    {
        IEnumerable<HistoricoPreco> GetHistoricos();
        void Post(HistoricoPreco historico);
        void Put(HistoricoPreco historico);
    }
}
