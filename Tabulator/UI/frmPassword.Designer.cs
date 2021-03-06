﻿namespace Tabulator.UI
{
    partial class frmPassword
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassword));
            this.btnVerifyPassword = new System.Windows.Forms.Button();
            this.txtPassword = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.panelLine = new System.Windows.Forms.Panel();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.panelRightPassword = new System.Windows.Forms.Panel();
            this.panelTopPassword = new System.Windows.Forms.Panel();
            this.panelLeftPassword = new System.Windows.Forms.Panel();
            this.panelBottomPassword = new System.Windows.Forms.Panel();
            this.bunifuElipse = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.SuspendLayout();
            // 
            // btnVerifyPassword
            // 
            this.btnVerifyPassword.BackColor = System.Drawing.Color.Teal;
            this.btnVerifyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerifyPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifyPassword.ForeColor = System.Drawing.Color.White;
            this.btnVerifyPassword.Location = new System.Drawing.Point(49, 176);
            this.btnVerifyPassword.Name = "btnVerifyPassword";
            this.btnVerifyPassword.Size = new System.Drawing.Size(294, 33);
            this.btnVerifyPassword.TabIndex = 24;
            this.btnVerifyPassword.TabStop = false;
            this.btnVerifyPassword.Text = "&Verify Password";
            this.btnVerifyPassword.UseVisualStyleBackColor = false;
            this.btnVerifyPassword.Click += new System.EventHandler(this.btnVerifyPassword_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtPassword.ForeColor = System.Drawing.Color.Teal;
            this.txtPassword.HintForeColor = System.Drawing.Color.Empty;
            this.txtPassword.HintText = "";
            this.txtPassword.isPassword = true;
            this.txtPassword.LineFocusedColor = System.Drawing.Color.Teal;
            this.txtPassword.LineIdleColor = System.Drawing.Color.Teal;
            this.txtPassword.LineMouseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(175)))), ((int)(((byte)(240)))));
            this.txtPassword.LineThickness = 3;
            this.txtPassword.Location = new System.Drawing.Point(49, 108);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(294, 33);
            this.txtPassword.TabIndex = 23;
            this.txtPassword.Text = "Password";
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.Teal;
            this.panelLine.Location = new System.Drawing.Point(104, 75);
            this.panelLine.Margin = new System.Windows.Forms.Padding(4);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(184, 1);
            this.panelLine.TabIndex = 22;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.Teal;
            this.lblPassword.Location = new System.Drawing.Point(135, 41);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(122, 30);
            this.lblPassword.TabIndex = 21;
            this.lblPassword.Text = "Password";
            // 
            // btnShutdown
            // 
            this.btnShutdown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShutdown.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnShutdown.FlatAppearance.BorderSize = 0;
            this.btnShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShutdown.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShutdown.ForeColor = System.Drawing.Color.White;
            this.btnShutdown.Image = ((System.Drawing.Image)(resources.GetObject("btnShutdown.Image")));
            this.btnShutdown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShutdown.Location = new System.Drawing.Point(343, 10);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(37, 31);
            this.btnShutdown.TabIndex = 20;
            this.btnShutdown.TabStop = false;
            this.btnShutdown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // panelRightPassword
            // 
            this.panelRightPassword.BackColor = System.Drawing.Color.Teal;
            this.panelRightPassword.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightPassword.Location = new System.Drawing.Point(382, 10);
            this.panelRightPassword.Margin = new System.Windows.Forms.Padding(4);
            this.panelRightPassword.Name = "panelRightPassword";
            this.panelRightPassword.Size = new System.Drawing.Size(10, 232);
            this.panelRightPassword.TabIndex = 19;
            // 
            // panelTopPassword
            // 
            this.panelTopPassword.BackColor = System.Drawing.Color.Teal;
            this.panelTopPassword.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopPassword.Location = new System.Drawing.Point(10, 0);
            this.panelTopPassword.Margin = new System.Windows.Forms.Padding(4);
            this.panelTopPassword.Name = "panelTopPassword";
            this.panelTopPassword.Size = new System.Drawing.Size(382, 10);
            this.panelTopPassword.TabIndex = 16;
            // 
            // panelLeftPassword
            // 
            this.panelLeftPassword.BackColor = System.Drawing.Color.Teal;
            this.panelLeftPassword.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftPassword.Location = new System.Drawing.Point(0, 0);
            this.panelLeftPassword.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftPassword.Name = "panelLeftPassword";
            this.panelLeftPassword.Size = new System.Drawing.Size(10, 242);
            this.panelLeftPassword.TabIndex = 18;
            // 
            // panelBottomPassword
            // 
            this.panelBottomPassword.BackColor = System.Drawing.Color.Teal;
            this.panelBottomPassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomPassword.Location = new System.Drawing.Point(0, 242);
            this.panelBottomPassword.Margin = new System.Windows.Forms.Padding(4);
            this.panelBottomPassword.Name = "panelBottomPassword";
            this.panelBottomPassword.Size = new System.Drawing.Size(392, 10);
            this.panelBottomPassword.TabIndex = 17;
            // 
            // bunifuElipse
            // 
            this.bunifuElipse.ElipseRadius = 25;
            this.bunifuElipse.TargetControl = this;
            // 
            // frmPassword
            // 
            this.AcceptButton = this.btnVerifyPassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnShutdown;
            this.ClientSize = new System.Drawing.Size(392, 252);
            this.Controls.Add(this.btnVerifyPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.panelLine);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.panelRightPassword);
            this.Controls.Add(this.panelTopPassword);
            this.Controls.Add(this.panelLeftPassword);
            this.Controls.Add(this.panelBottomPassword);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnVerifyPassword;
        private Bunifu.Framework.UI.BunifuMaterialTextbox txtPassword;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Panel panelRightPassword;
        private System.Windows.Forms.Panel panelTopPassword;
        private System.Windows.Forms.Panel panelLeftPassword;
        private System.Windows.Forms.Panel panelBottomPassword;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse;
    }
}