//    TrelloDriver - .NET library to interact with Trello using HTTP requests
//
//    Copyright(C) 2018  Dragos Circa
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//    
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see http://www.gnu.org/licenses.
namespace TrelloDriver.Forms
{
    partial class TrelloCardDisplay
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
            this.label1 = new System.Windows.Forms.Label();
            this.nameTb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.idTb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.boardIdTb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listIdTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.shortLinkTb = new System.Windows.Forms.TextBox();
            this.closeBtn = new System.Windows.Forms.Button();
            this.cardUrl = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // nameTb
            // 
            this.nameTb.Location = new System.Drawing.Point(140, 25);
            this.nameTb.Name = "nameTb";
            this.nameTb.ReadOnly = true;
            this.nameTb.Size = new System.Drawing.Size(328, 22);
            this.nameTb.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Id";
            // 
            // idTb
            // 
            this.idTb.Location = new System.Drawing.Point(140, 53);
            this.idTb.Name = "idTb";
            this.idTb.ReadOnly = true;
            this.idTb.Size = new System.Drawing.Size(328, 22);
            this.idTb.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Board Id";
            // 
            // boardIdTb
            // 
            this.boardIdTb.Location = new System.Drawing.Point(140, 81);
            this.boardIdTb.Name = "boardIdTb";
            this.boardIdTb.ReadOnly = true;
            this.boardIdTb.Size = new System.Drawing.Size(328, 22);
            this.boardIdTb.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "List Id";
            // 
            // listIdTb
            // 
            this.listIdTb.Location = new System.Drawing.Point(140, 109);
            this.listIdTb.Name = "listIdTb";
            this.listIdTb.ReadOnly = true;
            this.listIdTb.Size = new System.Drawing.Size(328, 22);
            this.listIdTb.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Members";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(140, 137);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(328, 100);
            this.listBox1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 271);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "URL";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Short Link";
            // 
            // shortLinkTb
            // 
            this.shortLinkTb.Location = new System.Drawing.Point(140, 243);
            this.shortLinkTb.Name = "shortLinkTb";
            this.shortLinkTb.ReadOnly = true;
            this.shortLinkTb.Size = new System.Drawing.Size(328, 22);
            this.shortLinkTb.TabIndex = 1;
            // 
            // closeBtn
            // 
            this.closeBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeBtn.Location = new System.Drawing.Point(337, 299);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(131, 30);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            // 
            // cardUrl
            // 
            this.cardUrl.AutoSize = true;
            this.cardUrl.Location = new System.Drawing.Point(137, 271);
            this.cardUrl.Name = "cardUrl";
            this.cardUrl.Size = new System.Drawing.Size(121, 17);
            this.cardUrl.TabIndex = 4;
            this.cardUrl.TabStop = true;
            this.cardUrl.Text = "www.example.com";
            this.cardUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cardUrl_LinkClicked);
            // 
            // TrelloCardDisplay
            // 
            this.AcceptButton = this.closeBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeBtn;
            this.ClientSize = new System.Drawing.Size(485, 341);
            this.Controls.Add(this.cardUrl);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.shortLinkTb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listIdTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.boardIdTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.idTb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameTb);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TrelloCardDisplay";
            this.Text = "TrelloCardDisplay";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TrelloCardDisplay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idTb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox boardIdTb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox listIdTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox shortLinkTb;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.LinkLabel cardUrl;
    }
}