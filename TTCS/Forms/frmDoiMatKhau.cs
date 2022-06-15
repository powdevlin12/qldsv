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
    public partial class frmDoiMatKhau : Form
    {
     
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();
        private void Lay_Logins(String cmd)
        {
            dt = new DataTable();
            if (Program.Conn.State == ConnectionState.Closed) Program.Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Program.Conn);
            da.Fill(dt);
            Program.Conn.Close();
            dgvDSLogin.DataSource = dt;
            this.dgvDSLogin.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvDSRole.AutoSize = true;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvDSLogin.CurrentCell.RowIndex;
            String loginName = dgvDSLogin.CurrentRow.Cells[0].Value.ToString();
            txtLoginName.Text = loginName;
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            
            if (Program.KetNoi() == 0) return;
            Lay_Logins("select * from v_dsLogin");

        }

        private void dgvDSLogin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvDSLogin.CurrentCell.RowIndex;
            String loginName = dgvDSLogin.CurrentRow.Cells[0].Value.ToString();
            txtLoginName.Text = loginName;
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string sqlCode = "sp_password "+txtMKCu.Text+" , "+txtMKMoi.Text+" , "+txtLoginName.Text;
            if (Program.ExecSqlNonQuery(sqlCode) == 0)
            {
                MessageBox.Show("Đổi mật khẩu thành công !");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!");
            } 
                
        }

        private void xoaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            String sqlCode = "exec SP_Lay_Thong_Tin " + txtLoginName.Text ;
            SqlDataReader myReader = Program.ExecSqlDataReader(sqlCode);
            myReader.Read();
            string userName = myReader.GetString(0);
            myReader.Close();

            sqlCode = "exec Xoa_Login " + txtLoginName.Text + " , " + userName;
            if (Program.ExecSqlNonQuery(sqlCode) == 0)
            {
                MessageBox.Show("Xoá Login thành công !");
                frmDoiMatKhau_Load(sender,e);
                txtLoginName.Text = "";
            }    
            else
                MessageBox.Show("Xoá Login thất bại !");
        }
    }
}
