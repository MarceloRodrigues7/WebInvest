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

namespace WinInvest.Investimento
{
    public partial class FrmInvestimento : Form
    {
        private IInvestimentoRepository repository = new InvestimentoRepository();
        private Usuario Usuario { get; set; }

        public FrmInvestimento(Usuario usuario)
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
            GridView.Columns[0].HeaderText = "Sigla";
            GridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[1].HeaderText = "Qtd.";
            GridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[2].HeaderText = "Preço Un.";
            GridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[3].HeaderText = "Preço T.";
            GridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[4].HeaderText = "D. Realização";
            GridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[5].HeaderText = "Status";
            GridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[6].HeaderText = "Tipo";
            GridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CarregaGridView()
        {
            GridView.DataSource = repository.GetInvestimentos(Usuario.Id);
            FormataGrid();
        }

        private void FrmInvestimento_Load(object sender, EventArgs e)
        {
            CarregaGridView();
        }
    }


}
