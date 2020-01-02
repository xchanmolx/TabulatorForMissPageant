namespace Tabulator.UI
{
    partial class frmAverageTop5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAverageTop5));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPrintAverageTop5 = new System.Windows.Forms.Button();
            this.lblSignatureJudge5 = new System.Windows.Forms.Label();
            this.lblSignatureJudge4 = new System.Windows.Forms.Label();
            this.lblSignatureJudge3 = new System.Windows.Forms.Label();
            this.lblSignatureJudge2 = new System.Windows.Forms.Label();
            this.lblSignatureJudge1 = new System.Windows.Forms.Label();
            this.lblSignatures = new System.Windows.Forms.Label();
            this.panelHead = new System.Windows.Forms.Panel();
            this.lblHead = new System.Windows.Forms.Label();
            this.lblRank5 = new System.Windows.Forms.Label();
            this.lblRank2 = new System.Windows.Forms.Label();
            this.lblRank1 = new System.Windows.Forms.Label();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.bunifuDataGridAverageTop5 = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.lblRank3 = new System.Windows.Forms.Label();
            this.lblRank4 = new System.Windows.Forms.Label();
            this.panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridAverageTop5)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrintAverageTop5
            // 
            this.btnPrintAverageTop5.BackColor = System.Drawing.Color.Maroon;
            this.btnPrintAverageTop5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnPrintAverageTop5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrintAverageTop5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintAverageTop5.Font = new System.Drawing.Font("Arial Narrow", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAverageTop5.ForeColor = System.Drawing.Color.White;
            this.btnPrintAverageTop5.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintAverageTop5.Image")));
            this.btnPrintAverageTop5.Location = new System.Drawing.Point(153, 568);
            this.btnPrintAverageTop5.Name = "btnPrintAverageTop5";
            this.btnPrintAverageTop5.Size = new System.Drawing.Size(542, 49);
            this.btnPrintAverageTop5.TabIndex = 238;
            this.btnPrintAverageTop5.TabStop = false;
            this.btnPrintAverageTop5.Text = "PRINT TOP 5 RESULT";
            this.btnPrintAverageTop5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintAverageTop5.UseVisualStyleBackColor = false;
            this.btnPrintAverageTop5.Visible = false;
            this.btnPrintAverageTop5.Click += new System.EventHandler(this.btnPrintAverageTop5_Click);
            // 
            // lblSignatureJudge5
            // 
            this.lblSignatureJudge5.AutoSize = true;
            this.lblSignatureJudge5.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge5.Name = "lblSignatureJudge5";
            this.lblSignatureJudge5.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge5.TabIndex = 249;
            this.lblSignatureJudge5.Text = "_________________________\r\n       Judge 5";
            this.lblSignatureJudge5.Visible = false;
            // 
            // lblSignatureJudge4
            // 
            this.lblSignatureJudge4.AutoSize = true;
            this.lblSignatureJudge4.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge4.Name = "lblSignatureJudge4";
            this.lblSignatureJudge4.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge4.TabIndex = 248;
            this.lblSignatureJudge4.Text = "_________________________\r\n       Judge 4";
            this.lblSignatureJudge4.Visible = false;
            // 
            // lblSignatureJudge3
            // 
            this.lblSignatureJudge3.AutoSize = true;
            this.lblSignatureJudge3.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge3.Name = "lblSignatureJudge3";
            this.lblSignatureJudge3.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge3.TabIndex = 247;
            this.lblSignatureJudge3.Text = "_________________________\r\n       Judge 3";
            this.lblSignatureJudge3.Visible = false;
            // 
            // lblSignatureJudge2
            // 
            this.lblSignatureJudge2.AutoSize = true;
            this.lblSignatureJudge2.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge2.Name = "lblSignatureJudge2";
            this.lblSignatureJudge2.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge2.TabIndex = 246;
            this.lblSignatureJudge2.Text = "_________________________\r\n       Judge 2";
            this.lblSignatureJudge2.Visible = false;
            // 
            // lblSignatureJudge1
            // 
            this.lblSignatureJudge1.AutoSize = true;
            this.lblSignatureJudge1.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge1.Name = "lblSignatureJudge1";
            this.lblSignatureJudge1.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge1.TabIndex = 244;
            this.lblSignatureJudge1.Text = "_________________________\r\n       Judge 1";
            this.lblSignatureJudge1.Visible = false;
            // 
            // lblSignatures
            // 
            this.lblSignatures.AutoSize = true;
            this.lblSignatures.Location = new System.Drawing.Point(150, 602);
            this.lblSignatures.Name = "lblSignatures";
            this.lblSignatures.Size = new System.Drawing.Size(66, 13);
            this.lblSignatures.TabIndex = 245;
            this.lblSignatures.Text = "Signatures : ";
            this.lblSignatures.Visible = false;
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.PeachPuff;
            this.panelHead.Controls.Add(this.lblHead);
            this.panelHead.Location = new System.Drawing.Point(12, 8);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(821, 38);
            this.panelHead.TabIndex = 237;
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
            // lblRank5
            // 
            this.lblRank5.AutoSize = true;
            this.lblRank5.Location = new System.Drawing.Point(680, 602);
            this.lblRank5.Name = "lblRank5";
            this.lblRank5.Size = new System.Drawing.Size(13, 13);
            this.lblRank5.TabIndex = 243;
            this.lblRank5.Text = "5";
            this.lblRank5.Visible = false;
            // 
            // lblRank2
            // 
            this.lblRank2.AutoSize = true;
            this.lblRank2.Location = new System.Drawing.Point(611, 602);
            this.lblRank2.Name = "lblRank2";
            this.lblRank2.Size = new System.Drawing.Size(13, 13);
            this.lblRank2.TabIndex = 240;
            this.lblRank2.Text = "2";
            this.lblRank2.Visible = false;
            // 
            // lblRank1
            // 
            this.lblRank1.AutoSize = true;
            this.lblRank1.Location = new System.Drawing.Point(588, 602);
            this.lblRank1.Name = "lblRank1";
            this.lblRank1.Size = new System.Drawing.Size(13, 13);
            this.lblRank1.TabIndex = 239;
            this.lblRank1.Text = "1";
            this.lblRank1.Visible = false;
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
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // bunifuDataGridAverageTop5
            // 
            this.bunifuDataGridAverageTop5.AllowUserToAddRows = false;
            this.bunifuDataGridAverageTop5.AllowUserToDeleteRows = false;
            this.bunifuDataGridAverageTop5.AllowUserToResizeColumns = false;
            this.bunifuDataGridAverageTop5.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTop5.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.bunifuDataGridAverageTop5.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bunifuDataGridAverageTop5.BackgroundColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTop5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuDataGridAverageTop5.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.bunifuDataGridAverageTop5.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuDataGridAverageTop5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.bunifuDataGridAverageTop5.ColumnHeadersHeight = 50;
            this.bunifuDataGridAverageTop5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.HotPink;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridAverageTop5.DefaultCellStyle = dataGridViewCellStyle6;
            this.bunifuDataGridAverageTop5.DoubleBuffered = true;
            this.bunifuDataGridAverageTop5.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.bunifuDataGridAverageTop5.EnableHeadersVisualStyles = false;
            this.bunifuDataGridAverageTop5.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.bunifuDataGridAverageTop5.HeaderForeColor = System.Drawing.Color.White;
            this.bunifuDataGridAverageTop5.Location = new System.Drawing.Point(12, 49);
            this.bunifuDataGridAverageTop5.Name = "bunifuDataGridAverageTop5";
            this.bunifuDataGridAverageTop5.ReadOnly = true;
            this.bunifuDataGridAverageTop5.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuDataGridAverageTop5.RowHeadersVisible = false;
            this.bunifuDataGridAverageTop5.RowTemplate.DividerHeight = 1;
            this.bunifuDataGridAverageTop5.RowTemplate.Height = 30;
            this.bunifuDataGridAverageTop5.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridAverageTop5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bunifuDataGridAverageTop5.Size = new System.Drawing.Size(821, 513);
            this.bunifuDataGridAverageTop5.TabIndex = 236;
            this.bunifuDataGridAverageTop5.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.bunifuDataGridAverageTop5_CellFormatting);
            // 
            // lblRank3
            // 
            this.lblRank3.AutoSize = true;
            this.lblRank3.Location = new System.Drawing.Point(634, 602);
            this.lblRank3.Name = "lblRank3";
            this.lblRank3.Size = new System.Drawing.Size(13, 13);
            this.lblRank3.TabIndex = 241;
            this.lblRank3.Text = "3";
            this.lblRank3.Visible = false;
            // 
            // lblRank4
            // 
            this.lblRank4.AutoSize = true;
            this.lblRank4.Location = new System.Drawing.Point(657, 602);
            this.lblRank4.Name = "lblRank4";
            this.lblRank4.Size = new System.Drawing.Size(13, 13);
            this.lblRank4.TabIndex = 242;
            this.lblRank4.Text = "4";
            this.lblRank4.Visible = false;
            // 
            // frmAverageTop5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(845, 624);
            this.Controls.Add(this.btnPrintAverageTop5);
            this.Controls.Add(this.lblSignatureJudge5);
            this.Controls.Add(this.lblSignatureJudge4);
            this.Controls.Add(this.lblSignatureJudge3);
            this.Controls.Add(this.lblSignatureJudge2);
            this.Controls.Add(this.lblSignatureJudge1);
            this.Controls.Add(this.lblSignatures);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.lblRank5);
            this.Controls.Add(this.lblRank2);
            this.Controls.Add(this.lblRank1);
            this.Controls.Add(this.bunifuDataGridAverageTop5);
            this.Controls.Add(this.lblRank3);
            this.Controls.Add(this.lblRank4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAverageTop5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOP 5 (RESULT)";
            this.Load += new System.EventHandler(this.frmAverageTop5_Load);
            this.panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridAverageTop5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnPrintAverageTop5;
        public System.Windows.Forms.Label lblSignatureJudge5;
        public System.Windows.Forms.Label lblSignatureJudge4;
        public System.Windows.Forms.Label lblSignatureJudge3;
        public System.Windows.Forms.Label lblSignatureJudge2;
        public System.Windows.Forms.Label lblSignatureJudge1;
        public System.Windows.Forms.Label lblSignatures;
        private System.Windows.Forms.Panel panelHead;
        public System.Windows.Forms.Label lblHead;
        public System.Windows.Forms.Label lblRank5;
        public System.Windows.Forms.Label lblRank2;
        public System.Windows.Forms.Label lblRank1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        public Bunifu.Framework.UI.BunifuCustomDataGrid bunifuDataGridAverageTop5;
        public System.Windows.Forms.Label lblRank3;
        public System.Windows.Forms.Label lblRank4;
    }
}