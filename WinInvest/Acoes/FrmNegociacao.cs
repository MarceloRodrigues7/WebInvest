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

namespace WinInvest.Acoes
{
    public partial class FrmNegociacao : Form
    {
        private readonly IAcaoRepository AcaoRepository = new AcaoRepository();
        private readonly IOrdemRepository OrdemRepository = new OrdemRepository();
        private Usuario Usuario { get; set; }
        private BaseAcao Acao { get; set; }

        public FrmNegociacao(string cod, Usuario usuario)
        {
            InitializeComponent();
            Acao = AcaoRepository.GetAcao(cod);
            Usuario = usuario;
        }

        private void FrmNegociacao_Load(object sender, EventArgs e)
        {
            txtCod.Text = Acao.Id.ToString();
            txtSigla.Text = Acao.Sigla;
            txtPreco.Text = $"R$ {Acao.ValorAtual}";
            txtNome.Text = Acao.Acao;
            txtDataCriacao.Text = Acao.DataCriacao.ToString("dd/MM/yyyy");
            txtDataAtualizacao.Text = Acao.DataAtualizacao.ToString();
            txtValorTotal.Text = $"R$ {Acao.ValorAtual}";

        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            var res = OrdemRepository.Compra(Usuario, Acao, Acao.ValorAtual * txtQuantidade.Value, Convert.ToInt32(txtQuantidade.Value));
            if (res)
            {
                Usuario.Saldo -= Acao.ValorAtual * txtQuantidade.Value;
                MessageBox.Show($"Compra de {Acao.Sigla} realizada!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            MessageBox.Show("Não foi possível realizar transferencia", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            var res = OrdemRepository.Venda(Usuario, Acao, Acao.ValorAtual * txtQuantidade.Value, Convert.ToInt32(txtQuantidade.Value));
            if (res)
            {
                Usuario.Saldo += Acao.ValorAtual * txtQuantidade.Value;
                MessageBox.Show($"Venda de {Acao.Sigla} realizada!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            MessageBox.Show("Não foi possível realizar transferencia", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void txtQuantidade_ValueChanged(object sender, EventArgs e)
        {
            txtValorTotal.Text = $"R$ {Acao.ValorAtual * txtQuantidade.Value}";
        }
    }
}
