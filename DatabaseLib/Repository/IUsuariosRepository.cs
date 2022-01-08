using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IUsuariosRepository
    {
        Usuario GetUsuario(long id);
        Usuario GetUsuario(Usuario usuario);
        decimal GetSaldoUsuario(long id);
        void Post(Usuario usuario);
        void PutSaldoUsuario(long idUsuario, decimal novoSaldo);
    }
}
