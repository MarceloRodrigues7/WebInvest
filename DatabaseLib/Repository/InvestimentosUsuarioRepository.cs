﻿using DatabaseLib.Context;
using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class InvestimentosUsuarioRepository : IInvestimentosUsuarioRepository
    {
        public int GetQuantidadeAcao(long idAcao, int idUsuario)
        {
            var context = new EntityDb();
            return context.InvestimentosUsuario
                .Where(i => i.UsuarioId == idUsuario && i.ProdutoId == idAcao)
                .Select(i => i.Quantidade)
                .FirstOrDefault();
        }

        public void PostInvestimentoUsuario(long idProduto, int idUsuario)
        {
            var context = new EntityDb();
            var data = new InvestimentoUsuario
            {
                ProdutoId = idProduto,
                UsuarioId = idUsuario,
                Quantidade = 0
            };
            context.Add(data);
            context.SaveChanges();
        }

        public void PutInvestimentoUsuario(long idProduto, int idUsuario, int quantidade)
        {
            var context = new EntityDb();
            var data = context.InvestimentosUsuario.Where(i => i.UsuarioId == idUsuario && i.ProdutoId == idProduto).FirstOrDefault();
            data.Quantidade = quantidade;
            context.Update(data);
            context.SaveChanges();
        }

        public bool ValidaAcaoUsuario(long idProduto, int idUsuario)
        {
            var context = new EntityDb();
            var res = context.InvestimentosUsuario.Where(i => i.UsuarioId == idUsuario && i.ProdutoId == idProduto);
            return res.Any();
        }
    }
}