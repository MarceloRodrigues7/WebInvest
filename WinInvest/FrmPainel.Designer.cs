
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInvestimentos = new System.Windows.Forms.Button();
            this.btnAcoes = new System.Windows.Forms.Button();
            this.btnTransferencias = new System.Windows.Forms.Button();
            this.btnConta = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // btnInvestimentos
            // 
            this.btnInvestimentos.Location = new System.Drawing.Point(-1, 92);
            this.btnInvestimentos.Name = "btnInvestimentos";
            this.btnInvestimentos.Size = new System.Drawing.Size(151, 32);
            this.btnInvestimentos.TabIndex = 0;
            this.btnInvestimentos.Text = "Investimentos";
            this.btnInvestimentos.UseVisualStyleBackColor = true;
            this.btnInvestimentos.Click += new System.EventHandler(this.btnInvestimentos_Click);
            // 
            // btnAcoes
            // 
            this.btnAcoes.Location = new System.Drawing.Point(-1, 130);
            this.btnAcoes.Name = "btnAcoes";
            this.btnAcoes.Size = new System.Drawing.Size(151, 32);
            this.btnAcoes.TabIndex = 1;
            this.btnAcoes.Text = "Ações";
            this.btnAcoes.UseVisualStyleBackColor = true;
            this.btnAcoes.Click += new System.EventHandler(this.btnAcoes_Click);
            // 
            // btnTransferencias
            // 
            this.btnTransferencias.Location = new System.Drawing.Point(-1, 168);
            this.btnTransferencias.Name = "btnTransferencias";
            this.btnTransferencias.Size = new System.Drawing.Size(151, 32);
            this.btnTransferencias.TabIndex = 2;
            this.btnTransferencias.Text = "Transferencias";
            this.btnTransferencias.UseVisualStyleBackColor = true;
            this.btnTransferencias.Click += new System.EventHandler(this.btnTransferencias_Click);
            // 
            // btnConta
            // 
            this.btnConta.Location = new System.Drawing.Point(-1, 54);
            this.btnConta.Name = "btnConta";
            this.btnConta.Size = new System.Drawing.Size(151, 32);
            this.btnConta.TabIndex = 3;
            this.btnConta.Text = "Conta";
            this.btnConta.UseVisualStyleBackColor = true;
            this.btnConta.Click += new System.EventHandler(this.btnConta_Click);
            // 
            // FrmPainel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 493);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "FrmPainel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Painel";
            this.Load += new System.EventHandler(this.FrmPainel_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConta;
        private System.Windows.Forms.Button btnTransferencias;
        private System.Windows.Forms.Button btnAcoes;
        private System.Windows.Forms.Button btnInvestimentos;
    }
}