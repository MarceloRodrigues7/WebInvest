// <auto-generated />
using System;
using DatabaseLib.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseLib.Migrations
{
    [DbContext(typeof(EntityDb))]
    [Migration("20220108172646_init_db")]
    partial class init_db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DatabaseLib.Domain.CategoriaLevel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<int>("LevelMax")
                        .HasColumnType("int");

                    b.Property<int>("LevelMin")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CategoriasLevel");
                });

            modelBuilder.Entity("DatabaseLib.Domain.HistoricoPreco", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<long>("ProdutoId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("HistoricoPrecos");
                });

            modelBuilder.Entity("DatabaseLib.Domain.InvestimentoUsuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("ProdutoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("InvestimentosUsuario");
                });

            modelBuilder.Entity("DatabaseLib.Domain.LevelUsuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<long>("CategoriaLevelId")
                        .HasColumnType("bigint");

                    b.Property<int>("ExpAtual")
                        .HasColumnType("int");

                    b.Property<int>("ExpProximo")
                        .HasColumnType("int");

                    b.Property<int>("LevelAtual")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaLevelId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("LevelUsuarios");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Ordem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<long>("IdAcao")
                        .HasColumnType("bigint");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("StatusOrdem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Tipo")
                        .HasColumnType("bit");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUn")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Ordens");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Produto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sigla")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TipoId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ValorAtual")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TipoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("DatabaseLib.Domain.TipoProduto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VariacaoMais")
                        .HasColumnType("int");

                    b.Property<int>("VariacaoMenos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TiposProduto");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sobrenome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("StatusAtivo")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DatabaseLib.Domain.HistoricoPreco", b =>
                {
                    b.HasOne("DatabaseLib.Domain.Produto", "Produto")
                        .WithMany("HistoricoPrecos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("DatabaseLib.Domain.InvestimentoUsuario", b =>
                {
                    b.HasOne("DatabaseLib.Domain.Produto", "Produto")
                        .WithMany("InvestimentosUsuario")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseLib.Domain.Usuario", "Usuario")
                        .WithMany("InvestimentosUsuario")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DatabaseLib.Domain.LevelUsuario", b =>
                {
                    b.HasOne("DatabaseLib.Domain.CategoriaLevel", "CategoriaLevel")
                        .WithMany("LevelUsuarios")
                        .HasForeignKey("CategoriaLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseLib.Domain.Usuario", "Usuario")
                        .WithMany("LevelUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaLevel");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Produto", b =>
                {
                    b.HasOne("DatabaseLib.Domain.TipoProduto", "Tipo")
                        .WithMany("Produtos")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("DatabaseLib.Domain.CategoriaLevel", b =>
                {
                    b.Navigation("LevelUsuarios");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Produto", b =>
                {
                    b.Navigation("HistoricoPrecos");

                    b.Navigation("InvestimentosUsuario");
                });

            modelBuilder.Entity("DatabaseLib.Domain.TipoProduto", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("DatabaseLib.Domain.Usuario", b =>
                {
                    b.Navigation("InvestimentosUsuario");

                    b.Navigation("LevelUsuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
