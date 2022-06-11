
namespace QLDSV.Forms
{
    partial class frmBackUp
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
            this.cbxLoaibu = new System.Windows.Forms.ComboBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn loại backup";
            // 
            // cbxLoaibu
            // 
            this.cbxLoaibu.FormattingEnabled = true;
            this.cbxLoaibu.Items.AddRange(new object[] {
            "Backup Full",
            "Backup Differential"});
            this.cbxLoaibu.Location = new System.Drawing.Point(229, 100);
            this.cbxLoaibu.Name = "cbxLoaibu";
            this.cbxLoaibu.Size = new System.Drawing.Size(269, 21);
            this.cbxLoaibu.TabIndex = 1;
            this.cbxLoaibu.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(300, 183);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 3;
            this.btnBackup.Text = "Back Up";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // frmBackUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 269);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.cbxLoaibu);
            this.Controls.Add(this.label1);
            this.Name = "frmBackUp";
            this.Text = "frmBackUp";
            this.Load += new System.EventHandler(this.frmBackUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxLoaibu;
        private System.Windows.Forms.Button btnBackup;
    }
}