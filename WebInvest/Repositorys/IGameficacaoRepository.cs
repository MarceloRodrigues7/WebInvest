using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Repositorys
{
    public interface IGameficacaoRepository
    {
        LevelUsuario GetLevelUsuario(string IdUsuario);
        int GetIdCategoriaPorLevel(int levelAtual);
        void PutExpAtual(int id, int expTotal);
        void AtualizaNovoLevel(int id, int novoLevel, int novoExpProximo, int novaCategoria);
    }
}
