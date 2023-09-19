using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Library_Management_System
{
    public partial class frmPublisher : Form
    {
        private SqlCommand cmd = new SqlCommand();
        private DBConnection conn = new DBConnection();
        private string sql;
        public frmPublisher()
        {
            InitializeComponent();
        }

        private void frmPublisher_Load(object sender, EventArgs e)
        {
            btnPublisher.Enabled = false;
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblBindingDetail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DA.SelectCommand = cmd;
                    DA.Fill(DT);
                    dgvPublisher.DataSource = DT;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.connection().Close();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashBoard home = new frmDashBoard();
            home.Show();
            this.Dispose();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            frmBook book = new frmBook();
            book.Show();
            this.Dispose();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            frmStudent student = new frmStudent();
            student.Show();
            this.Dispose();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategory category = new frmCategory();
            category.Show();
            this.Dispose();
        }

        private void dgvPublisher_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                txtPublisherID.Text = dgvPublisher.CurrentRow.Cells[0].Value.ToString();
                txtPublisherName.Text = dgvPublisher.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    if (txtPublisherName.Text.Length == 0)
                    {
                        MessageBox.Show("Publisher Name Is Required!");
                    }
                    else
                    {
                        sql = "INSERT INTO tblBindingDetail(Binding_Name) VALUES('" + txtPublisherName.Text + "')";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Publisher Data Added Successfully!");
                            txtPublisherName.Clear();
                            txtPublisherID.Clear();
                            sql = "SELECT * FROM tblBindingDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvPublisher.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Add Publisher Data!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.connection().Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    if (txtPublisherName.Text.Length == 0 || txtPublisherID.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required!");
                    }
                    else
                    {
                        sql = "UPDATE tblBindingDetail SET Binding_Name='" + txtPublisherName.Text + "' WHERE Binding_ID=" + int.Parse(txtPublisherID.Text);
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Publisher Detail Updated Successfully!");
                            txtPublisherID.Clear();
                            txtPublisherName.Clear();
                            sql = "SELECT * FROM tblBindingDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvPublisher.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Update Publisher Data!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.connection().Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    if (txtPublisherName.Text.Length == 0 || txtPublisherID.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required!");
                    }
                    else
                    {
                        sql = "DELETE FROM tblBindingDetail WHERE Binding_ID=" + int.Parse(txtPublisherID.Text);
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Publisher Detail Deleted Successfully!");
                            txtPublisherID.Clear();
                            txtPublisherName.Clear();
                            sql = "SELECT * FROM tblBindingDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvPublisher.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Delete Publisher Data!");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.connection().Close();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frmAccount account = new frmAccount();
            account.Show();
            this.Dispose();
        }
    }
}
