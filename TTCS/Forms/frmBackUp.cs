using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.Forms
{
    public partial class frmBackUp : Form
    {
        public frmBackUp()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
               
        }

        private void frmBackUp_Load(object sender, EventArgs e)
        {
            cbxLoaibu.SelectedIndex = 0;
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
       
            if (cbxLoaibu.SelectedIndex == 0)
            {
                if (Program.ExecSqlNonQuery("exec sp_backup_full") == 0) 
                     MessageBox.Show("Backup full database thành công !");
                else
                    MessageBox.Show("Backup thất bại");
            }
            else
            {
                if(Program.ExecSqlNonQuery("exec sp_backup_deffirential")==0)
                    MessageBox.Show("Backup Deffirential database thành công !");
                else
                    MessageBox.Show("Backup thất bại");
            }    
        }
    }
}
