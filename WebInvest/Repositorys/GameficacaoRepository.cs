using Dapper;
using DatabaseLib.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebInvest.Models;

namespace WebInvest.Repositorys
{
    public class GameficacaoRepository : IGameficacaoRepository
    {
        private readonly string _connectionString;

        public GameficacaoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DataServer");
        }

        public void AtualizaNovoLevel(long id, int novoLevel, int novoExpProximo, int novaCategoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"UPDATE LevelUsuarios SET LevelAtual=@novoLevel, ExpProximo=@novoExpProximo, IdCategoriaLevel=@novaCategoria WHERE Id=@id";
                connection.Open();
                connection.Execute(query, new { id, novoLevel, novoExpProximo, novaCategoria });
                connection.Close();
            };
        }

        public int GetIdCategoriaPorLevel(int levelAtual)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT Id FROM CategoriasLevel WITH(NOLOCK) WHERE LevelMin<=@levelAtual and LevelMax>@levelAtual-1";
                connection.Open();
                var data = connection.QueryFirst<int>(query, new { levelAtual });
                connection.Close();
                return data;
            };
        }

        public LevelUsuario GetLevelUsuario(string IdUsuario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT * FROM LevelUsuarios WITH(NOLOCK) WHERE IdUsuario=@IdUsuario";
                connection.Open();
                var data = connection.QueryFirst<LevelUsuario>(query, new { IdUsuario });
                connection.Close();
                return data;
            };
        }

        public void PutExpAtual(long id, int expTotal)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"UPDATE LevelUsuarios SET ExpAtual=@expTotal WHERE Id=@id";
                connection.Open();
                connection.Execute(query, new { id, expTotal });
                connection.Close();
            };
        }
    }
}
