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

namespace WinInvest
{
    public partial class FrmPainel : Form
    {
        private Usuario Usuario { get; set; }

        public FrmPainel(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            MessageBox.Show($"Bem vindo {Usuario.Nome}");
        }

        private void FrmPainel_Load(object sender, EventArgs e)
        {
            
        }

        #region Eventos para abrir formularios
        private void btnConta_Click(object sender, EventArgs e)
        {
            var test = this.MdiChildren.Where(m => m.Text == "Conta");
            if (test.Any())
            {
                MessageBox.Show("Formulario já está aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Conta.FrmConta frm = new Conta.FrmConta(Usuario);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnInvestimentos_Click(object sender, EventArgs e)
        {
            var test = this.MdiChildren.Where(m => m.Text == "Investimentos");
            if (test.Any())
            {
                MessageBox.Show("Formulario já está aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Investimento.FrmInvestimento frm = new Investimento.FrmInvestimento(Usuario);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnAcoes_Click(object sender, EventArgs e)
        {
            var test = this.MdiChildren.Where(m => m.Text == "Ações");
            if (test.Any())
            {
                MessageBox.Show("Formulario já está aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Acoes.FrmAcoes frm = new Acoes.FrmAcoes(Usuario);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnTransferencias_Click(object sender, EventArgs e)
        {
            var test = this.MdiChildren.Where(m => m.Text == "Transferencia");
            if (test.Any())
            {
                MessageBox.Show("Formulario já está aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Transferencia.FrmTransferencia frm = new Transferencia.FrmTransferencia();
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion
    }
}
