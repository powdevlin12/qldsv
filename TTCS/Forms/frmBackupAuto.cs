using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.Forms
{
    public partial class frmBackupAuto : Form
    {
        public frmBackupAuto()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"D:\Nam3Ki2\ttcs\backup\" + nameDevice.Text + ".bak";
            using (FileStream fs = File.Create(path)) ;
            String sqlStr = "EXEC sp_addumpdevice 'disk','" + nameDevice.Text + "','" + pathBackup.Text + "\\"+ nameDevice.Text+".bak'";
            if(Program.ExecSqlNonQuery(sqlStr) == 0)
            {             
                MessageBox.Show("Tạo backup device thành công !");
                SqlDataReader myreader = Program.ExecSqlDataReader("select * from sys.backup_devices");
                cmbDevice.Items.Clear();
                while (myreader.Read())
                {
                    string name = myreader.GetString(0);
                    cmbDevice.Items.Add(name);
                }
                myreader.Close();
                nameDevice.Text = "";
                pathBackup.Text = "";
            }
            else
            {
                File.Delete(path);
                MessageBox.Show("Tạo backup device thất bại !");
            } 
                
           
        }

        private void frmBackupAuto_Load(object sender, EventArgs e)
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
            SqlDataReader myreader1 = Program.ExecSqlDataReader("SELECT [name] FROM msdb.dbo.sysjobs");
            cmbNameJob.Items.Clear();
            while (myreader1.Read())
            {
                string name = myreader1.GetString(0);
                cmbNameJob.Items.Add(name);

            }
            cmbNameJob.SelectedIndex = 0;
        }

        private void cmbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog()== DialogResult.OK)
            {
                pathBackup.Text = folderBrowserDialog1.SelectedPath;
                pathBackup.Visible = true;
            }    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sqlString = "EXEC sp_tao_job_backup "+"'"+txtNameJob.Text+"',"+"'"+ "BACKUP DATABASE[QLDSV] TO["+cmbDevice.Text+"] WITH NOFORMAT, INIT, NAME = N''QLDSV - Full Database Backup'', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 "+ "'";
            if(Program.ExecSqlNonQuery(sqlString)==0)
            {
                MessageBox.Show("Tạo job thành công !");
                SqlDataReader myreader1 = Program.ExecSqlDataReader("SELECT [name] FROM msdb.dbo.sysjobs");
                cmbNameJob.Items.Clear();
                while (myreader1.Read())
                {
                    string name = myreader1.GetString(0);
                    cmbNameJob.Items.Add(name);

                }
                myreader1.Close();
                cmbNameJob.SelectedIndex = 0;
                txtNameJob.Text = "";
            }
            else
            {
                MessageBox.Show("Tạo job thất bại !");
            }    
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string sqlCode = "EXEC msdb.dbo.sp_start_job '" +cmbNameJob.Text+"'";
            if (Program.ExecSqlNonQuery(sqlCode) == 0)
            {
                MessageBox.Show("Chạy job thành công ! Đã backup dữ liệu");
            }else
            {
                MessageBox.Show("Chạy job thất bại !");
            }    
        }
    }
}
