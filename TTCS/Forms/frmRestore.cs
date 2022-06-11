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
    public partial class frmRestore : Form
    {
        public frmRestore()
        {
            InitializeComponent();
        }

        private void frmRestore_Load(object sender, EventArgs e)
        {
            SqlDataReader myreader = Program.ExecSqlDataReader("select * from sys.backup_devices");
            cmbDevice.Items.Clear();
            while (myreader.Read())
            {
                string name = myreader.GetString(0);
                cmbDevice.Items.Add(name);

            }
            cmbDevice.SelectedIndex = 0;
            myreader.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            string sqlCode = "use master exec  sp_restore_db '" + cmbDevice.Text+"'";
            Console.WriteLine(sqlCode);
            if(Program.ExecSqlNonQuery(sqlCode) == 0)
            {
                MessageBox.Show("Đã restore database thành công !");
            }
            else
            {
                MessageBox.Show("Restore database thất bại !");
            } 
                
        }
    }
}
