using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib.Domain
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool StatusAtivo { get; set; }

        public List<InvestimentoUsuario> InvestimentosUsuario { get; set; }
        public List<LevelUsuario> LevelUsuarios { get; set; }
    }
}
