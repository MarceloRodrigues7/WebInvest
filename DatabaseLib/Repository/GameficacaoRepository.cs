using DatabaseLib.Context;
using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class GameficacaoRepository : IGameficacaoRepository
    {
        public void AtualizaNovoLevel(long id, int novoLevel, int novoExpProximo, long novaCategoria)
        {
            var context = new EntityDb();
            var usuario = context.LevelUsuarios.Where(l => l.UsuarioId == id).FirstOrDefault();
            usuario.ExpProximo = novoExpProximo;
            usuario.CategoriaLevelId = novaCategoria;
            usuario.LevelAtual= novoLevel;
            context.SaveChanges();
        }

        public long GetIdCategoriaPorLevel(int levelAtual)
        {
            var context = new EntityDb();
            return context.CategoriasLevel.Where(l => l.LevelMin <= levelAtual && l.LevelMax > levelAtual - 1).FirstOrDefault().Id;
        }

        public LevelUsuario GetLevelUsuario(long id)
        {
            var context = new EntityDb();
            return context.LevelUsuarios.Where(u => u.UsuarioId == id).FirstOrDefault();
        }

        public void PutExpAtual(long id, int expTotal)
        {
            var context = new EntityDb();
            var usuario = context.LevelUsuarios.Where(u => u.UsuarioId == id).FirstOrDefault();
            usuario.ExpAtual=expTotal;
            context.SaveChanges();
        }
    }
}
