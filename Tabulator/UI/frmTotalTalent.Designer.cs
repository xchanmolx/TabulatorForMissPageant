namespace Tabulator.UI
{
    partial class frmTotalTalent
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTotalTalent));
            this.bunifuDataGridTotalTalent = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.panelHead = new System.Windows.Forms.Panel();
            this.lblHead = new System.Windows.Forms.Label();
            this.btnPrintTotalTalent = new System.Windows.Forms.Button();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.lblSignatureJudge1 = new System.Windows.Forms.Label();
            this.lblRank1 = new System.Windows.Forms.Label();
            this.lblRank2 = new System.Windows.Forms.Label();
            this.lblRank3 = new System.Windows.Forms.Label();
            this.lblSignatures = new System.Windows.Forms.Label();
            this.lblSignatureJudge2 = new System.Windows.Forms.Label();
            this.lblSignatureJudge3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridTotalTalent)).BeginInit();
            this.panelHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuDataGridTotalTalent
            // 
            this.bunifuDataGridTotalTalent.AllowUserToAddRows = false;
            this.bunifuDataGridTotalTalent.AllowUserToDeleteRows = false;
            this.bunifuDataGridTotalTalent.AllowUserToResizeColumns = false;
            this.bunifuDataGridTotalTalent.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalTalent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.bunifuDataGridTotalTalent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bunifuDataGridTotalTalent.BackgroundColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalTalent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuDataGridTotalTalent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.bunifuDataGridTotalTalent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuDataGridTotalTalent.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.bunifuDataGridTotalTalent.ColumnHeadersHeight = 50;
            this.bunifuDataGridTotalTalent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.HotPink;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridTotalTalent.DefaultCellStyle = dataGridViewCellStyle15;
            this.bunifuDataGridTotalTalent.DoubleBuffered = true;
            this.bunifuDataGridTotalTalent.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.bunifuDataGridTotalTalent.EnableHeadersVisualStyles = false;
            this.bunifuDataGridTotalTalent.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.bunifuDataGridTotalTalent.HeaderForeColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalTalent.Location = new System.Drawing.Point(12, 46);
            this.bunifuDataGridTotalTalent.Name = "bunifuDataGridTotalTalent";
            this.bunifuDataGridTotalTalent.ReadOnly = true;
            this.bunifuDataGridTotalTalent.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuDataGridTotalTalent.RowHeadersVisible = false;
            this.bunifuDataGridTotalTalent.RowTemplate.DividerHeight = 1;
            this.bunifuDataGridTotalTalent.RowTemplate.Height = 30;
            this.bunifuDataGridTotalTalent.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridTotalTalent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bunifuDataGridTotalTalent.Size = new System.Drawing.Size(821, 513);
            this.bunifuDataGridTotalTalent.TabIndex = 1;
            this.bunifuDataGridTotalTalent.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.bunifuDataGridTotalTalent_CellFormatting);
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
            // btnPrintTotalTalent
            // 
            this.btnPrintTotalTalent.BackColor = System.Drawing.Color.Maroon;
            this.btnPrintTotalTalent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnPrintTotalTalent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrintTotalTalent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTotalTalent.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintTotalTalent.ForeColor = System.Drawing.Color.White;
            this.btnPrintTotalTalent.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintTotalTalent.Image")));
            this.btnPrintTotalTalent.Location = new System.Drawing.Point(153, 565);
            this.btnPrintTotalTalent.Name = "btnPrintTotalTalent";
            this.btnPrintTotalTalent.Size = new System.Drawing.Size(542, 49);
            this.btnPrintTotalTalent.TabIndex = 179;
            this.btnPrintTotalTalent.TabStop = false;
            this.btnPrintTotalTalent.Text = "PRINT BEST IN TALENT RESULT";
            this.btnPrintTotalTalent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintTotalTalent.UseVisualStyleBackColor = false;
            this.btnPrintTotalTalent.Visible = false;
            this.btnPrintTotalTalent.Click += new System.EventHandler(this.btnPrintTotalTalent_Click);
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
            // frmTotalTalent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(845, 624);
            this.Controls.Add(this.btnPrintTotalTalent);
            this.Controls.Add(this.lblSignatureJudge3);
            this.Controls.Add(this.lblSignatureJudge2);
            this.Controls.Add(this.lblSignatureJudge1);
            this.Controls.Add(this.lblSignatures);
            this.Controls.Add(this.lblRank3);
            this.Controls.Add(this.lblRank2);
            this.Controls.Add(this.lblRank1);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.bunifuDataGridTotalTalent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTotalTalent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BEST IN TALENT (RESULT)";
            this.Load += new System.EventHandler(this.frmTotalTalent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridTotalTalent)).EndInit();
            this.panelHead.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Bunifu.Framework.UI.BunifuCustomDataGrid bunifuDataGridTotalTalent;
        private System.Windows.Forms.Panel panelHead;
        public System.Windows.Forms.Label lblHead;
        public System.Windows.Forms.Button btnPrintTotalTalent;
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