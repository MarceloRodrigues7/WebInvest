using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IInvestimentosUsuarioRepository
    {
        bool ValidaAcaoUsuario(long idAcao, int idUsuario);
        void PostInvestimentoUsuario(long idAcao, int idUsuario);
        int GetQuantidadeAcao(long idAcao, int idUsuario);
        void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade);
    }
}
