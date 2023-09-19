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
    public partial class frmCategory : Form
    {
        private DBConnection conn = new DBConnection();
        private SqlCommand cmd = new SqlCommand();
        private string sql;
        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            btnCategory.Enabled = false;
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblCategory";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DA.SelectCommand = cmd;
                    DA.Fill(DT);
                    dgvCategory.DataSource = DT;
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                txtCategoryID.Text = dgvCategory.CurrentRow.Cells[0].Value.ToString();
                txtCategoryName.Text = dgvCategory.CurrentRow.Cells[1].Value.ToString();
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
                    if (txtCategoryName.Text.Length == 0)
                    {
                        MessageBox.Show("Category Name Is Required!");
                    }
                    else
                    {
                        sql = "INSERT INTO tblCategory(Category_Name) VALUES('" + txtCategoryName.Text + "')";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Category Added Successfully!");
                            txtCategoryID.Clear();
                            txtCategoryName.Clear();
                            sql = "SELECT * FROM tblCategory";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvCategory.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Insert Category!");
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
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
                if(conn.connection().State == ConnectionState.Open)
                {
                    if(txtCategoryID.Text.Length == 0)
                    {
                        MessageBox.Show("Category Id Is Required To Delete Category Data!");
                    }
                    else
                    {
                        sql = "DELETE FROM tblCategory WHERE Category_ID=" + int.Parse(txtCategoryID.Text);
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Category Data Is Deleted Successfully!");
                            txtCategoryID.Clear();
                            txtCategoryName.Clear();
                            sql = "SELECT * FROM tblCategory";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvCategory.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Delete Category Data!");
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch(Exception ex)
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
                    if (txtCategoryID.Text.Length == 0 || txtCategoryName.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required To Update Category Data!");
                    }
                    else
                    {
                        sql = "UPDATE tblCategory SET Category_Name='" + txtCategoryName.Text + "' WHERE Category_ID=" + int.Parse(txtCategoryID.Text);
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Category Data Is Updated Successfully!");
                            txtCategoryID.Clear();
                            txtCategoryName.Clear();
                            sql = "SELECT * FROM tblCategory";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvCategory.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Update Category Data!");
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

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            frmPublisher publisher = new frmPublisher();
            publisher.Show();
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
