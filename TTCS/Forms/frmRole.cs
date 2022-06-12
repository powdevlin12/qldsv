using DevExpress.XtraEditors;
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
    public partial class frmRole : Form
    {
        public frmRole()
        {
            InitializeComponent();
        }
        private DataTable dt = new DataTable();
        private void frmRole_Load(object sender, EventArgs e)
        {
            listQuyen.SelectionMode = SelectionMode.MultiSimple;
            if (Program.KetNoi() == 0) return;
            Lay_Role("select * from v_getRoles");
        }
        private void Lay_Role(String cmd)
        {
            dt = new DataTable();
            if (Program.Conn.State == ConnectionState.Closed) Program.Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, Program.Conn);
            da.Fill(dt);
            Program.Conn.Close();
            dgvDSRole.DataSource = dt;
            this.dgvDSRole.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dgvDSRole.AutoSize = true;
        }

        private void addRow(String roleInf)
        {
            DataRow newRow = dt.NewRow();
            newRow["name"] = roleInf;
            dt.Rows.Add(newRow);
            dt.AcceptChanges();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtRole.Text.Trim().Equals(""))
            {
                XtraMessageBox.Show("Bạn chưa điền tên Role !!!", "Lỗi tạo role", MessageBoxButtons.OK);
                txtRole.Focus();
            }
            else if (txtRole.Text.Length >= 20)
            {
                XtraMessageBox.Show("Tên Role không được quá 20 kí tự !!!", "Lỗi tạo role", MessageBoxButtons.OK);
                txtRole.Focus();
            }
            else if (listQuyen.SelectedIndex == -1)
            {
                XtraMessageBox.Show("Bạn chưa chọn quyền cho Role !!!", "Lỗi tạo role", MessageBoxButtons.OK);
            }
            else
            {
                String strLenh = " EXEC    @return_value = sp_checkRoleName " + txtRole.Text.Trim();
                strLenh = " DECLARE @return_value int " + strLenh +
                        " SELECT  'Return Value' = @return_value ";

                int resultCheckLogin = Utils.CheckDataHelper(strLenh);
                if (resultCheckLogin == 1)
                {
                    XtraMessageBox.Show("Tên Role đã tồn tại, vui lòng điền tên khác !!!", "Lỗi tạo role", MessageBoxButtons.OK);
                    txtRole.Focus();
                }
                else
                {
                    strLenh = "exec sp_addrole " + txtRole.Text;
                    Program.ExecSqlNonQuery(strLenh);
                    foreach (String i in listQuyen.SelectedItems)
                    {
                        strLenh = "exec sp_addrolemember " + i + ", " + txtRole.Text.Trim();
                        Program.ExecSqlNonQuery(strLenh);
                    }

                    addRow(txtRole.Text.Trim());
                    dgvDSRole.CurrentCell = dgvDSRole.Rows[dgvDSRole.Rows.Count - 1].Cells[0];
                    XtraMessageBox.Show("Thêm Role thành công !!!", "", MessageBoxButtons.OK);
                    txtRole.Text = "";
                    listQuyen.SelectionMode = SelectionMode.None;
                    listQuyen.SelectionMode = SelectionMode.MultiSimple;
                }
            }
        }

        private void xoaRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgvDSRole.CurrentCell.RowIndex;
            String roleName = dgvDSRole.CurrentRow.Cells[0].Value.ToString();
            String strLenh = " EXEC    @return_value = sp_checkRoleUsed " + roleName;
            strLenh = " DECLARE @return_value int " + strLenh +
                    " SELECT  'Return Value' = @return_value ";
            int resultCheckLogin = Utils.CheckDataHelper(strLenh);

            if (resultCheckLogin == 0)
            {
                dgvDSRole.Rows.RemoveAt(index);
                strLenh = "exec sp_droprole " + roleName;
                Program.ExecSqlNonQuery(strLenh);
            }
            else
            {
                XtraMessageBox.Show("Role " + roleName + " đã được sử dụng !!!", "", MessageBoxButtons.OK);
            }
        }

        private void refeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtRole.Text = "";
            listQuyen.SelectionMode = SelectionMode.None;
            frmRole_Load(sender, e);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtRole.Text = "";
            listQuyen.SelectionMode = SelectionMode.None;
            frmRole_Load(sender, e);
        }
    }
}
