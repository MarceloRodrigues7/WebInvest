using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Repositorys
{
    public interface IUsuariosRepository
    {
        decimal GetSaldoUsuario(int id);
        void PutSaldoUsuario(long idUsuario, decimal novoSaldo);
    }
}
