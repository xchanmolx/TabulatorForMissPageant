namespace Tabulator.UI
{
    partial class frmAverageTalent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAverageTalent));
            this.bunifuDataGridAverageTalent = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.panelHead = new System.Windows.Forms.Panel();
            this.lblHead = new System.Windows.Forms.Label();
            this.btnPrintAverageTalent = new System.Windows.Forms.Button();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.lblSignatureJudge1 = new System.Windows.Forms.Label();
            this.lblRank1 = new System.Windows.Forms.Label();
            this.lblRank2 = new System.Windows.Forms.Label();
            this.lblRank3 = new System.Windows.Forms.Label();
            this.lblSignatures = new System.Windows.Forms.Label();
            this.lblSignatureJudge2 = new System.Windows.Forms.Label();
            this.lblSignatureJudge3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridAverageTalent)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuDataGridAverageTalent
            // 
            this.bunifuDataGridAverageTalent.AllowUserToAddRows = false;
            this.bunifuDataGridAverageTalent.AllowUserToDeleteRows = false;
            this.bunifuDataGridAverageTalent.AllowUserToResizeColumns = false;
            this.bunifuDataGridAverageTalent.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTalent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.bunifuDataGridAverageTalent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bunifuDataGridAverageTalent.BackgroundColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTalent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuDataGridAverageTalent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.bunifuDataGridAverageTalent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuDataGridAverageTalent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.bunifuDataGridAverageTalent.ColumnHeadersHeight = 50;
            this.bunifuDataGridAverageTalent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.HotPink;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridAverageTalent.DefaultCellStyle = dataGridViewCellStyle3;
            this.bunifuDataGridAverageTalent.DoubleBuffered = true;
            this.bunifuDataGridAverageTalent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.bunifuDataGridAverageTalent.EnableHeadersVisualStyles = false;
            this.bunifuDataGridAverageTalent.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.bunifuDataGridAverageTalent.HeaderForeColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTalent.Location = new System.Drawing.Point(12, 46);
            this.bunifuDataGridAverageTalent.Name = "bunifuDataGridAverageTalent";
            this.bunifuDataGridAverageTalent.ReadOnly = true;
            this.bunifuDataGridAverageTalent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuDataGridAverageTalent.RowHeadersVisible = false;
            this.bunifuDataGridAverageTalent.RowTemplate.DividerHeight = 1;
            this.bunifuDataGridAverageTalent.RowTemplate.Height = 30;
            this.bunifuDataGridAverageTalent.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridAverageTalent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bunifuDataGridAverageTalent.Size = new System.Drawing.Size(821, 513);
            this.bunifuDataGridAverageTalent.TabIndex = 1;
            this.bunifuDataGridAverageTalent.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.bunifuDataGridAverageTalent_CellFormatting);
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.PeachPuff;
            this.panelHead.Controls.Add(this.lblHead);
            this.panelHead.Location = new System.Drawing.Point(12, 5);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(821, 38);
            this.panelHead.TabIndex = 16;
            // 
            // lblHead
            // 
            this.lblHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHead.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHead.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblHead.Location = new System.Drawing.Point(0, 0);
            this.lblHead.Name = "lblHead";
            this.lblHead.Size = new System.Drawing.Size(821, 38);
            this.lblHead.TabIndex = 16;
            this.lblHead.Text = "?";
            this.lblHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrintAverageTalent
            // 
            this.btnPrintAverageTalent.BackColor = System.Drawing.Color.Maroon;
            this.btnPrintAverageTalent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnPrintAverageTalent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrintAverageTalent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintAverageTalent.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAverageTalent.ForeColor = System.Drawing.Color.White;
            this.btnPrintAverageTalent.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintAverageTalent.Image")));
            this.btnPrintAverageTalent.Location = new System.Drawing.Point(153, 565);
            this.btnPrintAverageTalent.Name = "btnPrintAverageTalent";
            this.btnPrintAverageTalent.Size = new System.Drawing.Size(542, 49);
            this.btnPrintAverageTalent.TabIndex = 179;
            this.btnPrintAverageTalent.TabStop = false;
            this.btnPrintAverageTalent.Text = "PRINT BEST IN TALENT RESULT";
            this.btnPrintAverageTalent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintAverageTalent.UseVisualStyleBackColor = false;
            this.btnPrintAverageTalent.Visible = false;
            this.btnPrintAverageTalent.Click += new System.EventHandler(this.btnPrintAverageTalent_Click);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // lblSignatureJudge1
            // 
            this.lblSignatureJudge1.AutoSize = true;
            this.lblSignatureJudge1.Location = new System.Drawing.Point(150, 588);
            this.lblSignatureJudge1.Name = "lblSignatureJudge1";
            this.lblSignatureJudge1.Size = new System.Drawing.Size(193, 26);
            this.lblSignatureJudge1.TabIndex = 180;
            this.lblSignatureJudge1.Text = "_______________________________\r\n          Judge 1";
            this.lblSignatureJudge1.Visible = false;
            // 
            // lblRank1
            // 
            this.lblRank1.AutoSize = true;
            this.lblRank1.Location = new System.Drawing.Point(630, 601);
            this.lblRank1.Name = "lblRank1";
            this.lblRank1.Size = new System.Drawing.Size(13, 13);
            this.lblRank1.TabIndex = 181;
            this.lblRank1.Text = "1";
            this.lblRank1.Visible = false;
            // 
            // lblRank2
            // 
            this.lblRank2.AutoSize = true;
            this.lblRank2.Location = new System.Drawing.Point(655, 601);
            this.lblRank2.Name = "lblRank2";
            this.lblRank2.Size = new System.Drawing.Size(13, 13);
            this.lblRank2.TabIndex = 182;
            this.lblRank2.Text = "2";
            this.lblRank2.Visible = false;
            // 
            // lblRank3
            // 
            this.lblRank3.AutoSize = true;
            this.lblRank3.Location = new System.Drawing.Point(680, 601);
            this.lblRank3.Name = "lblRank3";
            this.lblRank3.Size = new System.Drawing.Size(13, 13);
            this.lblRank3.TabIndex = 183;
            this.lblRank3.Text = "3";
            this.lblRank3.Visible = false;
            // 
            // lblSignatures
            // 
            this.lblSignatures.AutoSize = true;
            this.lblSignatures.Location = new System.Drawing.Point(150, 601);
            this.lblSignatures.Name = "lblSignatures";
            this.lblSignatures.Size = new System.Drawing.Size(66, 13);
            this.lblSignatures.TabIndex = 184;
            this.lblSignatures.Text = "Signatures : ";
            this.lblSignatures.Visible = false;
            // 
            // lblSignatureJudge2
            // 
            this.lblSignatureJudge2.AutoSize = true;
            this.lblSignatureJudge2.Location = new System.Drawing.Point(150, 588);
            this.lblSignatureJudge2.Name = "lblSignatureJudge2";
            this.lblSignatureJudge2.Size = new System.Drawing.Size(193, 26);
            this.lblSignatureJudge2.TabIndex = 185;
            this.lblSignatureJudge2.Text = "_______________________________\r\n          Judge 2";
            this.lblSignatureJudge2.Visible = false;
            // 
            // lblSignatureJudge3
            // 
            this.lblSignatureJudge3.AutoSize = true;
            this.lblSignatureJudge3.Location = new System.Drawing.Point(150, 588);
            this.lblSignatureJudge3.Name = "lblSignatureJudge3";
            this.lblSignatureJudge3.Size = new System.Drawing.Size(193, 26);
            this.lblSignatureJudge3.TabIndex = 186;
            this.lblSignatureJudge3.Text = "_______________________________\r\n          Judge 3";
            this.lblSignatureJudge3.Visible = false;
            // 
            // frmAverageTalent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(845, 624);
            this.Controls.Add(this.btnPrintAverageTalent);
            this.Controls.Add(this.lblSignatureJudge3);
            this.Controls.Add(this.lblSignatureJudge2);
            this.Controls.Add(this.lblSignatureJudge1);
            this.Controls.Add(this.lblSignatures);
            this.Controls.Add(this.lblRank3);
            this.Controls.Add(this.lblRank2);
            this.Controls.Add(this.lblRank1);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.bunifuDataGridAverageTalent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAverageTalent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BEST IN TALENT (RESULT)";
            this.Load += new System.EventHandler(this.frmAverageTalent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridAverageTalent)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Bunifu.Framework.UI.BunifuCustomDataGrid bunifuDataGridAverageTalent;
        private System.Windows.Forms.Panel panelHead;
        public System.Windows.Forms.Label lblHead;
        public System.Windows.Forms.Button btnPrintAverageTalent;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        public System.Windows.Forms.Label lblSignatureJudge1;
        public System.Windows.Forms.Label lblRank1;
        public System.Windows.Forms.Label lblRank2;
        public System.Windows.Forms.Label lblRank3;
        public System.Windows.Forms.Label lblSignatures;
        public System.Windows.Forms.Label lblSignatureJudge2;
        public System.Windows.Forms.Label lblSignatureJudge3;
    }
}