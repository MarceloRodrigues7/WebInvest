using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinInvest.Repositorys;

namespace WinInvest
{
    public partial class FrmLogin : Form
    {
        private IUsuarioRepository repository = new UsuarioRepository();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                var res = repository.GetUsuario(txtUsername.Text, txtPassword.Text);
                if (res==null)
                {
                    MessageBox.Show("Usuário e/ou Senha inválido!", "Atenção");
                    return;
                }
                FrmPainel frm = new FrmPainel(res);
                this.Hide();
                frm.Show();
            }
        }

        private bool ValidaCampos()
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Username em branco!", "Atenção");
                txtUsername.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password em branco!", "Atenção");
                txtPassword.Focus();
                return false;
            }
            return true;
        }
    }
}
