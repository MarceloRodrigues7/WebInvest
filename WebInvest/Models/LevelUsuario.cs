using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebInvest.Models
{
    public class LevelUsuario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int LevelAtual { get; set; }
        public int ExpAtual { get; set; }
        public int ExpProximo { get; set; }
        public string Categoria { get; set; }
        public int IdCategoriaLevel { get; set; }

        public static int GanhaExp = 1;

    }
}
