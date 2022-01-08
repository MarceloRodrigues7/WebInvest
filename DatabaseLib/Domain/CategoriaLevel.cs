using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class CategoriaLevel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int LevelMin { get; set; }
        public int LevelMax { get; set; }

        public List<LevelUsuario> LevelUsuarios { get; set; }
    }
}
