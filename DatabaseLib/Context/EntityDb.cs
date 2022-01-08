using DatabaseLib.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Context
{
    public class EntityDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(DbConnection.ConnectionString);

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<TipoProduto> TiposProduto { get; set; }
        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<HistoricoPreco> HistoricoPrecos { get; set; }
        public DbSet<InvestimentoUsuario> InvestimentosUsuario { get; set; }
        public DbSet<CategoriaLevel> CategoriasLevel { get; set;}
        public DbSet<LevelUsuario> LevelUsuarios { get; set; }

    }
}
