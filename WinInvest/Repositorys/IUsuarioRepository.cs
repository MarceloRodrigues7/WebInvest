using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinInvest.Models;

namespace WinInvest.Repositorys
{
    public interface IUsuarioRepository
    {
        Usuario GetUsuario(string username, string password);
    }
}
