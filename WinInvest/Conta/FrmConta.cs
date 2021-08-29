using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinInvest.Models;
using WinInvest.Repositorys;

namespace WinInvest.Conta
{
    public partial class FrmConta : Form
    {
        private Usuario Usuario { get; set; }

        public FrmConta(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
        }

        private void FrmConta_Load(object sender, EventArgs e)
        {
            txtCod.Text = Usuario.Id.ToString();
            txtUsername.Text = Usuario.Username;
            txtPassword.Text = Usuario.Password;
            txtEmail.Text = Usuario.Email;
            txtNome.Text = Usuario.Nome;
            txtSobrenome.Text = Usuario.Sobrenome;
            txtDataNascimento.Text = Usuario.DataNascimento.ToString();
            txtDataAlteração.Text = Usuario.DataAlteracao.ToString();
            txtAtivo.Text = Usuario.StatusAtivo.ToString();
            Usuario.Saldo = new Usuario().AtualizaSaldo(Usuario.Id);
            txtSaldo.Text = $"R$ {Usuario.Saldo}";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }
    }
}
