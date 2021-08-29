
namespace WinInvest
{
    partial class FrmPainel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPainel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnConta = new System.Windows.Forms.Button();
            this.btnTransferencias = new System.Windows.Forms.Button();
            this.btnAcoes = new System.Windows.Forms.Button();
            this.btnInvestimentos = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSaldo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripAcoes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripUltAtualizacao = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnConta);
            this.panel1.Controls.Add(this.btnTransferencias);
            this.panel1.Controls.Add(this.btnAcoes);
            this.panel1.Controls.Add(this.btnInvestimentos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 493);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinInvest.Properties.Resources.online_shop;
            this.pictureBox1.Location = new System.Drawing.Point(28, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(91, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnConta
            // 
            this.btnConta.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConta.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnConta.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnConta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnConta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnConta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConta.Image = global::WinInvest.Properties.Resources.management;
            this.btnConta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConta.Location = new System.Drawing.Point(-1, 195);
            this.btnConta.Name = "btnConta";
            this.btnConta.Size = new System.Drawing.Size(151, 38);
            this.btnConta.TabIndex = 3;
            this.btnConta.Text = "Conta";
            this.btnConta.UseVisualStyleBackColor = false;
            this.btnConta.Click += new System.EventHandler(this.btnConta_Click);
            // 
            // btnTransferencias
            // 
            this.btnTransferencias.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTransferencias.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTransferencias.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTransferencias.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnTransferencias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnTransferencias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferencias.Image = global::WinInvest.Properties.Resources.money;
            this.btnTransferencias.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransferencias.Location = new System.Drawing.Point(-1, 158);
            this.btnTransferencias.Name = "btnTransferencias";
            this.btnTransferencias.Size = new System.Drawing.Size(151, 38);
            this.btnTransferencias.TabIndex = 2;
            this.btnTransferencias.Text = "Transferencias";
            this.btnTransferencias.UseVisualStyleBackColor = false;
            this.btnTransferencias.Click += new System.EventHandler(this.btnTransferencias_Click);
            // 
            // btnAcoes
            // 
            this.btnAcoes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAcoes.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAcoes.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnAcoes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnAcoes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAcoes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcoes.Image = global::WinInvest.Properties.Resources.graphic;
            this.btnAcoes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcoes.Location = new System.Drawing.Point(-1, 120);
            this.btnAcoes.Name = "btnAcoes";
            this.btnAcoes.Size = new System.Drawing.Size(151, 39);
            this.btnAcoes.TabIndex = 1;
            this.btnAcoes.Text = "Ações";
            this.btnAcoes.UseVisualStyleBackColor = false;
            this.btnAcoes.Click += new System.EventHandler(this.btnAcoes_Click);
            // 
            // btnInvestimentos
            // 
            this.btnInvestimentos.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnInvestimentos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnInvestimentos.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.ControlLight;
            this.btnInvestimentos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue;
            this.btnInvestimentos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnInvestimentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvestimentos.Image = global::WinInvest.Properties.Resources.savings;
            this.btnInvestimentos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvestimentos.Location = new System.Drawing.Point(-1, 83);
            this.btnInvestimentos.Name = "btnInvestimentos";
            this.btnInvestimentos.Size = new System.Drawing.Size(151, 38);
            this.btnInvestimentos.TabIndex = 0;
            this.btnInvestimentos.Text = "Investimentos";
            this.btnInvestimentos.UseVisualStyleBackColor = false;
            this.btnInvestimentos.Click += new System.EventHandler(this.btnInvestimentos_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUsuario,
            this.toolStripSaldo,
            this.toolStripAcoes,
            this.toolStripUltAtualizacao});
            this.statusStrip1.Location = new System.Drawing.Point(151, 471);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripUsuario
            // 
            this.toolStripUsuario.Name = "toolStripUsuario";
            this.toolStripUsuario.Size = new System.Drawing.Size(50, 17);
            this.toolStripUsuario.Text = "Usuário:";
            // 
            // toolStripSaldo
            // 
            this.toolStripSaldo.Name = "toolStripSaldo";
            this.toolStripSaldo.Size = new System.Drawing.Size(39, 17);
            this.toolStripSaldo.Text = "Saldo:";
            // 
            // toolStripAcoes
            // 
            this.toolStripAcoes.Name = "toolStripAcoes";
            this.toolStripAcoes.Size = new System.Drawing.Size(106, 17);
            this.toolStripAcoes.Text = "Ações em Sistema:";
            // 
            // toolStripUltAtualizacao
            // 
            this.toolStripUltAtualizacao.Name = "toolStripUltAtualizacao";
            this.toolStripUltAtualizacao.Size = new System.Drawing.Size(92, 17);
            this.toolStripUltAtualizacao.Text = "Ult. Atualização:";
            // 
            // FrmPainel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 493);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FrmPainel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Painel";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPainel_FormClosed);
            this.Load += new System.EventHandler(this.FrmPainel_Load);
            this.MouseEnter += new System.EventHandler(this.FrmPainel_MouseEnter);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConta;
        private System.Windows.Forms.Button btnTransferencias;
        private System.Windows.Forms.Button btnAcoes;
        private System.Windows.Forms.Button btnInvestimentos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripUsuario;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSaldo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripAcoes;
        private System.Windows.Forms.ToolStripStatusLabel toolStripUltAtualizacao;
    }
}