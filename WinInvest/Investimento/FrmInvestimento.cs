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
        private readonly IInvestimentoRepository repository = new InvestimentoRepository();
        private readonly IAcaoRepository acaoRepository = new AcaoRepository();
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
            GridView.Columns[0].HeaderText = "Cod.";
            GridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[1].HeaderText = "Sigla";
            GridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[2].HeaderText = "Qtd";
            GridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[3].HeaderText = "Valor Atual";
            GridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[4].HeaderText = "D. Atualização";
            GridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            GridView.Columns[5].HeaderText = "Ao Vender";
            GridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void CarregaGridView()
        {
            GridView.DataSource = null;
            GridView.DataSource = repository.GetInvestimentos(Usuario.Id);
            FormataGrid();
        }

        private void FrmInvestimento_Load(object sender, EventArgs e)
        {
            CarregaGridView();
            cbSigla.DataSource = acaoRepository.GetAcoesSiglas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregaGridView();
        }
    }
}
