using DatabaseLib.Context;
using DatabaseLib.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class LevelUsuariosRepository : ILevelUsuariosRepository
    {
        public LevelUsuario GetLevelECategoriaUsuario(long idUsuario)
        {
            var context = new EntityDb();
            return context.LevelUsuarios.Where(l => l.UsuarioId == idUsuario).Include(l => l.CategoriaLevel).FirstOrDefault();
        }

        public void PostNovoUsuario(long idUsuario)
        {
            var context = new EntityDb();
            var data = new LevelUsuario
            {
                ExpAtual = 0,
                LevelAtual = 1,
                CategoriaLevelId = 1,
                UsuarioId = idUsuario,
                ExpProximo = 5,
            };
            context.LevelUsuarios.Add(data);
            context.SaveChanges();
        }

        public bool ValidaUsuarioTabelaLevel(long idUsuario)
        {
            var context = new EntityDb();
            return context.LevelUsuarios.Where(l => l.UsuarioId == idUsuario).Any();
        }
    }
}
