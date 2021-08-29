using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinInvest.Models;
using WinInvest.Repositorys;

namespace WinInvest
{
    public partial class FrmPainel : Form
    {
        private IAcaoRepository acaoRepository = new AcaoRepository();
        private IUsuarioRepository usuarioRepository = new UsuarioRepository();
        private Usuario Usuario { get; set; }

        public FrmPainel(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            MessageBox.Show($"Bem vindo {Usuario.Nome}");
        }

        private void FrmPainel_Load(object sender, EventArgs e)
        {
            AtualizaToolStrip();
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
            Transferencia.FrmTransferencia frm = new Transferencia.FrmTransferencia(Usuario);
            frm.MdiParent = this;
            frm.Show();
        }
        #endregion

        private void FrmPainel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void AtualizaToolStrip()
        {
                Usuario = usuarioRepository.GetUsuario(Usuario.Username, Usuario.Password);
                var tAcoes = acaoRepository.GetAcoesSiglas().Count();
                toolStripUsuario.Text = $"Usuário: {Usuario.Nome}";
                toolStripSaldo.Text = $"Saldo: R$ {Usuario.Saldo}";
                toolStripAcoes.Text = $"Ações em Sistema: {tAcoes}";
                toolStripUltAtualizacao.Text = $"Ult. Atualização: {DateTime.Now}";
        }

        private void FrmPainel_MouseEnter(object sender, EventArgs e)
        {
            AtualizaToolStrip();
        }
    }
}
