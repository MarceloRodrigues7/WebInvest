using DatabaseLib.Context;
using DatabaseLib.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Repository
{
    public class UsuariosRepository : IUsuariosRepository
    {
        public Usuario GetUsuario(long id)
        {
            var context = new EntityDb();
            return context.Usuarios.Where(u => u.Id == id).FirstOrDefault();
        }

        public Usuario GetUsuario(Usuario usuario)
        {
            var context = new EntityDb();
            return context.Usuarios.Where(u => u.Username == usuario.Username && u.Password == usuario.Password).FirstOrDefault();
        }

        public void Post(Usuario usuario)
        {
            var context = new EntityDb();
            context.Add(usuario);
            context.SaveChanges();
        }

        public decimal GetSaldoUsuario(long id)
        {
            var context = new EntityDb();
            return context.Usuarios.Where(u => u.Id == id).FirstOrDefault().Saldo;
        }

        public void PutSaldoUsuario(long idUsuario, decimal novoSaldo)
        {
            var usuario = GetUsuario(idUsuario);
            usuario.Saldo = novoSaldo;
            var context = new EntityDb();
            context.Usuarios.Update(usuario);
        }
    }
}
