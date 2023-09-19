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
    public partial class frmBook : Form
    {
        private DBConnection conn = new DBConnection();
        private SqlCommand cmd = new SqlCommand();
        private string sql;
        public frmBook()
        {
            InitializeComponent();
        }
        private void loadCategory()
        {
            SqlDataReader DR;
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblCategory";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while(DR.Read())
                    {
                        cboCategory.Items.Add(DR.GetValue(1).ToString());
                    }
                    DR.Close();
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
        private void loadPublisher()
        {
            SqlDataReader DR;
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblBindingDetail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        cboPublisher.Items.Add(DR.GetValue(1).ToString());
                    }
                    DR.Close();
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
        private void frmBook_Load(object sender, EventArgs e)
        {
            loadCategory();
            loadPublisher();
            btnBook.Enabled = false;
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM Book_Detail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DA.SelectCommand = cmd;
                    DA.Fill(DT);
                    dgvBook.DataSource = DT;
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

        private void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                txtISBN.Text = dgvBook.CurrentRow.Cells[0].Value.ToString();
                txtTitle.Text = dgvBook.CurrentRow.Cells[1].Value.ToString();
                cboCategory.Text = dgvBook.CurrentRow.Cells[2].Value.ToString();
                cboPublisher.Text = dgvBook.CurrentRow.Cells[3].Value.ToString();
                txtPYear.Text = dgvBook.CurrentRow.Cells[4].Value.ToString();
                txtANumber.Text = dgvBook.CurrentRow.Cells[5].Value.ToString();
                txtCNumber.Text = dgvBook.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            frmDashBoard dashboard = new frmDashBoard();
            dashboard.Show();
            this.Dispose();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            SqlDataReader DR;
            int catID=0, pubID=0;
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT Category_ID FROM tblCategory WHERE Category_Name='" + cboCategory.Text + "'";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while(DR.Read())
                    {
                        catID = DR.GetInt32(0);
                    }
                    DR.Close();
                    sql = "SELECT Binding_ID FROM tblBindingDetail WHERE Binding_Name='" + cboPublisher.Text + "'";
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        pubID = DR.GetInt32(0);
                    }
                    DR.Close();
                    if(txtISBN.Text.Length == 0 || txtTitle.Text.Length==0 || txtPYear.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required To Process");
                    }
                    else
                    {
                        sql = "INSERT INTO tblBookDetail(ISBN,Book_Title,Category,Binding_ID,Publcation_Year,Actual_No_Of_Copy,Current_No_Of_Copy)" +
                            "VALUES('" + txtISBN.Text + "','" + txtTitle.Text + "'," + catID + "," + pubID + "," + int.Parse(txtPYear.Text) + "," + int.Parse(txtANumber.Text) +
                            "," + txtCNumber.Text + ")";
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Book Added Successfully!");
                            txtISBN.Clear();
                            txtTitle.Clear();
                            txtPYear.Clear();
                            txtANumber.Clear();
                            txtCNumber.Clear();
                            cboPublisher.ResetText();
                            cboCategory.ResetText();
                            sql = "SELECT * FROM tblBookDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvBook.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Error: Unable To Add Book!");
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
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "DELETE FROM tblBookDetail WHERE ISBN='" + txtISBN.Text + "'";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Book Deleted Successfully!");
                        txtISBN.Clear();
                        txtTitle.Clear();
                        txtPYear.Clear();
                        txtANumber.Clear();
                        txtCNumber.Clear();
                        cboPublisher.ResetText();
                        cboCategory.ResetText();
                        sql = "SELECT * FROM tblBookDetail";
                        cmd.CommandText = sql;
                        DA.SelectCommand = cmd;
                        DA.Fill(DT);
                        dgvBook.DataSource = DT;
                    }
                    else
                    {
                        MessageBox.Show("Error: Unable To Delete Book!");
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
            SqlDataReader DR;
            int pubID = 0, catID = 0;
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT Category_ID FROM tblCategory WHERE Category_Name='" + cboCategory.Text + "'";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        catID = DR.GetInt32(0);
                    }
                    DR.Close();
                    sql = "SELECT Binding_ID FROM tblBindingDetail WHERE Binding_Name='" + cboPublisher.Text + "'";
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        pubID = DR.GetInt32(0);
                    }
                    DR.Close();
                    if (txtISBN.Text.Length == 0 || txtTitle.Text.Length == 0 || txtPYear.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required To Process");
                    }
                    else
                    {
                        sql = "UPDATE tblBookDetail SET Book_Title='" + txtTitle.Text + "', Category=" + catID + ", Binding_ID=" + pubID + ", Publcation_Year=" + int.Parse(txtPYear.Text) +
                            ", Actual_No_Of_Copy=" + int.Parse(txtANumber.Text) + ", Current_No_Of_Copy=" + int.Parse(txtCNumber.Text) + " WHERE ISBN='" + txtISBN.Text + "'";
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Book Updated Successfully!");
                            txtISBN.Clear();
                            txtTitle.Clear();
                            txtPYear.Clear();
                            txtANumber.Clear();
                            txtCNumber.Clear();
                            cboPublisher.ResetText();
                            cboCategory.ResetText();
                            sql = "SELECT * FROM tblBookDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvBook.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Error: Unable To Update Book!");
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
