using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IInvestimentosUsuarioRepository
    {
        IEnumerable<InvestimentoUsuario> GetInvestimentoUsuarios(long idUsuario);
        bool ValidaAcaoUsuario(long idAcao, long idUsuario);
        void PostInvestimentoUsuario(long idAcao, long idUsuario);
        int GetQuantidadeAcao(long idAcao, long idUsuario);
        void PutInvestimentoUsuario(long idAcao, long idUsuario, int quantidade);
    }
}
