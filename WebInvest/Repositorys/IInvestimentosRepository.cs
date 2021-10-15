using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Repositorys
{
    public interface IInvestimentosRepository
    {
        void PutInvestimentoUsuario(long idAcao, int idUsuario, int quantidade);
        int GetQuantidadeAcaoUsuario(long idAcao, int idUsuario);
        bool ValidacaoInvestimentoUsuario(long idAcao, int idUsuario);
    }
}
