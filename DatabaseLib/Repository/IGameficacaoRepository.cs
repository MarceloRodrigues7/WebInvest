using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface IGameficacaoRepository
    {
        LevelUsuario GetLevelUsuario(long id);
        long GetIdCategoriaPorLevel(int levelAtual);
        void PutExpAtual(long id, int expTotal);
        void AtualizaNovoLevel(long id, int novoLevel, int novoExpProximo, long novaCategoria);
    }
}
