using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class LevelUsuario
    {
        public long Id { get; set; }
        public long UsuarioId { get; set; }
        public long CategoriaLevelId { get; set; }
        public int LevelAtual { get; set; }
        public int ExpAtual { get; set; }
        public int ExpProximo { get; set; }
        

        public Usuario Usuario { get; set; }
        public CategoriaLevel CategoriaLevel { get; set; }
    }
}
