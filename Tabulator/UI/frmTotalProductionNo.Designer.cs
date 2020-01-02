namespace Tabulator.UI
{
    partial class frmTotalProductionNo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTotalProductionNo));
            this.panelHead = new System.Windows.Forms.Panel();
            this.lblHead = new System.Windows.Forms.Label();
            this.bunifuDataGridTotalProductionNo = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.btnPrintTotalProductionNo = new System.Windows.Forms.Button();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.lblRank3 = new System.Windows.Forms.Label();
            this.lblRank2 = new System.Windows.Forms.Label();
            this.lblRank1 = new System.Windows.Forms.Label();
            this.lblRank5 = new System.Windows.Forms.Label();
            this.lblRank4 = new System.Windows.Forms.Label();
            this.lblSignatureJudge5 = new System.Windows.Forms.Label();
            this.lblSignatureJudge4 = new System.Windows.Forms.Label();
            this.lblSignatureJudge3 = new System.Windows.Forms.Label();
            this.lblSignatureJudge2 = new System.Windows.Forms.Label();
            this.lblSignatureJudge1 = new System.Windows.Forms.Label();
            this.lblSignatures = new System.Windows.Forms.Label();
            this.panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridTotalProductionNo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHead
            // 
            this.panelHead.BackColor = System.Drawing.Color.PeachPuff;
            this.panelHead.Controls.Add(this.lblHead);
            this.panelHead.Location = new System.Drawing.Point(12, 8);
            this.panelHead.Name = "panelHead";
            this.panelHead.Size = new System.Drawing.Size(821, 38);
            this.panelHead.TabIndex = 185;
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
            // bunifuDataGridTotalProductionNo
            // 
            this.bunifuDataGridTotalProductionNo.AllowUserToAddRows = false;
            this.bunifuDataGridTotalProductionNo.AllowUserToDeleteRows = false;
            this.bunifuDataGridTotalProductionNo.AllowUserToResizeColumns = false;
            this.bunifuDataGridTotalProductionNo.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalProductionNo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.bunifuDataGridTotalProductionNo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.bunifuDataGridTotalProductionNo.BackgroundColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalProductionNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuDataGridTotalProductionNo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.bunifuDataGridTotalProductionNo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuDataGridTotalProductionNo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.bunifuDataGridTotalProductionNo.ColumnHeadersHeight = 50;
            this.bunifuDataGridTotalProductionNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.HotPink;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridTotalProductionNo.DefaultCellStyle = dataGridViewCellStyle12;
            this.bunifuDataGridTotalProductionNo.DoubleBuffered = true;
            this.bunifuDataGridTotalProductionNo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.bunifuDataGridTotalProductionNo.EnableHeadersVisualStyles = false;
            this.bunifuDataGridTotalProductionNo.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.bunifuDataGridTotalProductionNo.HeaderForeColor = System.Drawing.Color.White;
            this.bunifuDataGridTotalProductionNo.Location = new System.Drawing.Point(12, 49);
            this.bunifuDataGridTotalProductionNo.Name = "bunifuDataGridTotalProductionNo";
            this.bunifuDataGridTotalProductionNo.ReadOnly = true;
            this.bunifuDataGridTotalProductionNo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.bunifuDataGridTotalProductionNo.RowHeadersVisible = false;
            this.bunifuDataGridTotalProductionNo.RowTemplate.DividerHeight = 1;
            this.bunifuDataGridTotalProductionNo.RowTemplate.Height = 30;
            this.bunifuDataGridTotalProductionNo.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuDataGridTotalProductionNo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.bunifuDataGridTotalProductionNo.Size = new System.Drawing.Size(821, 513);
            this.bunifuDataGridTotalProductionNo.TabIndex = 184;
            this.bunifuDataGridTotalProductionNo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.bunifuDataGridTotalProductionNo_CellFormatting);
            // 
            // btnPrintTotalProductionNo
            // 
            this.btnPrintTotalProductionNo.BackColor = System.Drawing.Color.Maroon;
            this.btnPrintTotalProductionNo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnPrintTotalProductionNo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPrintTotalProductionNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintTotalProductionNo.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintTotalProductionNo.ForeColor = System.Drawing.Color.White;
            this.btnPrintTotalProductionNo.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintTotalProductionNo.Image")));
            this.btnPrintTotalProductionNo.Location = new System.Drawing.Point(153, 568);
            this.btnPrintTotalProductionNo.Name = "btnPrintTotalProductionNo";
            this.btnPrintTotalProductionNo.Size = new System.Drawing.Size(542, 49);
            this.btnPrintTotalProductionNo.TabIndex = 186;
            this.btnPrintTotalProductionNo.TabStop = false;
            this.btnPrintTotalProductionNo.Text = "PRINT BEST IN PRODUCTION NUMBER RESULT";
            this.btnPrintTotalProductionNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintTotalProductionNo.UseVisualStyleBackColor = false;
            this.btnPrintTotalProductionNo.Visible = false;
            this.btnPrintTotalProductionNo.Click += new System.EventHandler(this.btnPrintTotalProductionNo_Click);
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
            // lblRank3
            // 
            this.lblRank3.AutoSize = true;
            this.lblRank3.Location = new System.Drawing.Point(634, 602);
            this.lblRank3.Name = "lblRank3";
            this.lblRank3.Size = new System.Drawing.Size(13, 13);
            this.lblRank3.TabIndex = 190;
            this.lblRank3.Text = "3";
            this.lblRank3.Visible = false;
            // 
            // lblRank2
            // 
            this.lblRank2.AutoSize = true;
            this.lblRank2.Location = new System.Drawing.Point(611, 602);
            this.lblRank2.Name = "lblRank2";
            this.lblRank2.Size = new System.Drawing.Size(13, 13);
            this.lblRank2.TabIndex = 189;
            this.lblRank2.Text = "2";
            this.lblRank2.Visible = false;
            // 
            // lblRank1
            // 
            this.lblRank1.AutoSize = true;
            this.lblRank1.Location = new System.Drawing.Point(588, 602);
            this.lblRank1.Name = "lblRank1";
            this.lblRank1.Size = new System.Drawing.Size(13, 13);
            this.lblRank1.TabIndex = 188;
            this.lblRank1.Text = "1";
            this.lblRank1.Visible = false;
            // 
            // lblRank5
            // 
            this.lblRank5.AutoSize = true;
            this.lblRank5.Location = new System.Drawing.Point(680, 602);
            this.lblRank5.Name = "lblRank5";
            this.lblRank5.Size = new System.Drawing.Size(13, 13);
            this.lblRank5.TabIndex = 192;
            this.lblRank5.Text = "5";
            this.lblRank5.Visible = false;
            // 
            // lblRank4
            // 
            this.lblRank4.AutoSize = true;
            this.lblRank4.Location = new System.Drawing.Point(657, 602);
            this.lblRank4.Name = "lblRank4";
            this.lblRank4.Size = new System.Drawing.Size(13, 13);
            this.lblRank4.TabIndex = 191;
            this.lblRank4.Text = "4";
            this.lblRank4.Visible = false;
            // 
            // lblSignatureJudge5
            // 
            this.lblSignatureJudge5.AutoSize = true;
            this.lblSignatureJudge5.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge5.Name = "lblSignatureJudge5";
            this.lblSignatureJudge5.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge5.TabIndex = 198;
            this.lblSignatureJudge5.Text = "_________________________\r\n       Judge 5";
            this.lblSignatureJudge5.Visible = false;
            // 
            // lblSignatureJudge4
            // 
            this.lblSignatureJudge4.AutoSize = true;
            this.lblSignatureJudge4.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge4.Name = "lblSignatureJudge4";
            this.lblSignatureJudge4.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge4.TabIndex = 197;
            this.lblSignatureJudge4.Text = "_________________________\r\n       Judge 4";
            this.lblSignatureJudge4.Visible = false;
            // 
            // lblSignatureJudge3
            // 
            this.lblSignatureJudge3.AutoSize = true;
            this.lblSignatureJudge3.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge3.Name = "lblSignatureJudge3";
            this.lblSignatureJudge3.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge3.TabIndex = 196;
            this.lblSignatureJudge3.Text = "_________________________\r\n       Judge 3";
            this.lblSignatureJudge3.Visible = false;
            // 
            // lblSignatureJudge2
            // 
            this.lblSignatureJudge2.AutoSize = true;
            this.lblSignatureJudge2.Location = new System.Drawing.Point(150, 589);
            this.lblSignatureJudge2.Name = "lblSignatureJudge2";
            this.lblSignatureJudge2.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge2.TabIndex = 195;
            this.lblSignatureJudge2.Text = "_________________________\r\n       Judge 2";
            this.lblSignatureJudge2.Visible = false;
            // 
            // lblSignatureJudge1
            // 
            this.lblSignatureJudge1.AutoSize = true;
            this.lblSignatureJudge1.Location = new System.Drawing.Point(150, 591);
            this.lblSignatureJudge1.Name = "lblSignatureJudge1";
            this.lblSignatureJudge1.Size = new System.Drawing.Size(157, 26);
            this.lblSignatureJudge1.TabIndex = 193;
            this.lblSignatureJudge1.Text = "_________________________\r\n       Judge 1";
            this.lblSignatureJudge1.Visible = false;
            // 
            // lblSignatures
            // 
            this.lblSignatures.AutoSize = true;
            this.lblSignatures.Location = new System.Drawing.Point(150, 604);
            this.lblSignatures.Name = "lblSignatures";
            this.lblSignatures.Size = new System.Drawing.Size(66, 13);
            this.lblSignatures.TabIndex = 194;
            this.lblSignatures.Text = "Signatures : ";
            this.lblSignatures.Visible = false;
            // 
            // frmTotalProductionNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(210)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(845, 624);
            this.Controls.Add(this.btnPrintTotalProductionNo);
            this.Controls.Add(this.lblSignatureJudge5);
            this.Controls.Add(this.lblSignatureJudge4);
            this.Controls.Add(this.lblSignatureJudge3);
            this.Controls.Add(this.lblSignatureJudge2);
            this.Controls.Add(this.lblSignatureJudge1);
            this.Controls.Add(this.lblSignatures);
            this.Controls.Add(this.lblRank5);
            this.Controls.Add(this.lblRank4);
            this.Controls.Add(this.panelHead);
            this.Controls.Add(this.bunifuDataGridTotalProductionNo);
            this.Controls.Add(this.lblRank3);
            this.Controls.Add(this.lblRank2);
            this.Controls.Add(this.lblRank1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTotalProductionNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BEST IN PRODUCTION NUMBER (RESULT)";
            this.Load += new System.EventHandler(this.frmTotalProductionNo_Load);
            this.panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuDataGridTotalProductionNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHead;
        public System.Windows.Forms.Label lblHead;
        public Bunifu.Framework.UI.BunifuCustomDataGrid bunifuDataGridTotalProductionNo;
        public System.Windows.Forms.Button btnPrintTotalProductionNo;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        public System.Windows.Forms.Label lblRank3;
        public System.Windows.Forms.Label lblRank2;
        public System.Windows.Forms.Label lblRank1;
        public System.Windows.Forms.Label lblRank5;
        public System.Windows.Forms.Label lblRank4;
        public System.Windows.Forms.Label lblSignatureJudge5;
        public System.Windows.Forms.Label lblSignatureJudge4;
        public System.Windows.Forms.Label lblSignatureJudge3;
        public System.Windows.Forms.Label lblSignatureJudge2;
        public System.Windows.Forms.Label lblSignatureJudge1;
        public System.Windows.Forms.Label lblSignatures;
    }
}