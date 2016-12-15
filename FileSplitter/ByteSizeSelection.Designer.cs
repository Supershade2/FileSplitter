namespace FileSplitter
{
    partial class ByteSizeSelection
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SelectionText = new System.Windows.Forms.Label();
            this.GigaByte = new System.Windows.Forms.RadioButton();
            this.MegaBytes = new System.Windows.Forms.RadioButton();
            this.KiloByte = new System.Windows.Forms.RadioButton();
            this.Literal = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.73913F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.26087F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.Controls.Add(this.SelectionText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.GigaByte, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MegaBytes, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.KiloByte, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Literal, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(339, 74);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // SelectionText
            // 
            this.SelectionText.AutoSize = true;
            this.SelectionText.BackColor = System.Drawing.Color.Transparent;
            this.SelectionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectionText.Location = new System.Drawing.Point(122, 0);
            this.SelectionText.Name = "SelectionText";
            this.SelectionText.Size = new System.Drawing.Size(105, 37);
            this.SelectionText.TabIndex = 0;
            this.SelectionText.Text = "\r\nSelect byte size\r\n";
            this.SelectionText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GigaByte
            // 
            this.GigaByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.GigaByte.AutoSize = true;
            this.GigaByte.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GigaByte.Location = new System.Drawing.Point(36, 40);
            this.GigaByte.Name = "GigaByte";
            this.GigaByte.Size = new System.Drawing.Size(46, 31);
            this.GigaByte.TabIndex = 1;
            this.GigaByte.TabStop = true;
            this.GigaByte.Text = "GB";
            this.GigaByte.UseVisualStyleBackColor = true;
            this.GigaByte.CheckedChanged += new System.EventHandler(this.GigaByte_CheckedChanged);
            // 
            // MegaBytes
            // 
            this.MegaBytes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.MegaBytes.AutoSize = true;
            this.MegaBytes.Location = new System.Drawing.Point(154, 40);
            this.MegaBytes.Name = "MegaBytes";
            this.MegaBytes.Size = new System.Drawing.Size(41, 31);
            this.MegaBytes.TabIndex = 2;
            this.MegaBytes.TabStop = true;
            this.MegaBytes.Text = "MB";
            this.MegaBytes.UseVisualStyleBackColor = true;
            this.MegaBytes.CheckedChanged += new System.EventHandler(this.MegaBytes_CheckedChanged);
            // 
            // KiloByte
            // 
            this.KiloByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.KiloByte.AutoSize = true;
            this.KiloByte.Location = new System.Drawing.Point(236, 40);
            this.KiloByte.Name = "KiloByte";
            this.KiloByte.Size = new System.Drawing.Size(39, 31);
            this.KiloByte.TabIndex = 3;
            this.KiloByte.TabStop = true;
            this.KiloByte.Text = "KB";
            this.KiloByte.UseVisualStyleBackColor = true;
            this.KiloByte.CheckedChanged += new System.EventHandler(this.KiloByte_CheckedChanged);
            // 
            // Literal
            // 
            this.Literal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Literal.AutoSize = true;
            this.Literal.Location = new System.Drawing.Point(285, 40);
            this.Literal.Name = "Literal";
            this.Literal.Size = new System.Drawing.Size(51, 31);
            this.Literal.TabIndex = 4;
            this.Literal.TabStop = true;
            this.Literal.Text = "None";
            this.Literal.UseVisualStyleBackColor = true;
            this.Literal.CheckedChanged += new System.EventHandler(this.Literal_CheckedChanged);
            // 
            // ByteSizeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 74);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ByteSizeSelection";
            this.ShowIcon = false;
            this.Text = "ByteSizeSelection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ByteSizeSelection_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label SelectionText;
        private System.Windows.Forms.RadioButton GigaByte;
        private System.Windows.Forms.RadioButton MegaBytes;
        private System.Windows.Forms.RadioButton KiloByte;
        private System.Windows.Forms.RadioButton Literal;
    }
}