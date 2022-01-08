using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public interface ILevelUsuariosRepository
    {
        public LevelUsuario GetLevelECategoriaUsuario(long idUsuario);
        bool ValidaUsuarioTabelaLevel(long idUsuario);
        void PostNovoUsuario(long idUsuario);
        IEnumerable<LevelUsuario> GetLevelECategoriaUsuarios();
    }
}
