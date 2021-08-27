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
    public partial class FrmAcoes : Form
    {
        private IAcaoRepository repository = new AcaoRepository();
        private Usuario Usuario { get; set; }

        public FrmAcoes(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
        }

        private void FormataGrid()
        {
            GridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
            GridView.AllowUserToAddRows = false;
            GridView.AllowUserToDeleteRows = false;
            GridView.AllowUserToResizeRows = false;
            GridView.AllowUserToOrderColumns = true;
            GridView.ReadOnly = true;
            GridView.RowsDefaultCellStyle.BackColor = Color.LightCyan;
            GridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            GridView.Columns[0].HeaderText = "Cod";
            GridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[2].HeaderText = "Nome";
            GridView.Columns[2].Width = 150;
            GridView.Columns[3].HeaderText = "Preço";
            GridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[4].HeaderText = "D. Atualização";
            GridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[5].HeaderText = "D. Criação";
            GridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CarregaGridView()
        {
            GridView.DataSource = repository.GetAcoes();
            FormataGrid();
        }

        private void FrmAcoes_Load(object sender, EventArgs e)
        {
            CarregaGridView();
        }

        private void GridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var Cod = GridView.SelectedCells[0].Value.ToString();
            FrmNegociacao frm = new FrmNegociacao(Cod,Usuario);
            frm.ShowDialog();
            Usuario.Saldo = new Usuario().AtualizaSaldo(Usuario.Id);
        }
    }
}
