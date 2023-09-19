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
    public partial class frmStudent : Form
    {
        private DBConnection conn = new DBConnection();
        private SqlCommand cmd = new SqlCommand();
        private string sql;
        public frmStudent()
        {
            InitializeComponent();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            btnStudent.Enabled = false;
            SqlDataAdapter DA = new SqlDataAdapter();
            DataTable DT = new DataTable();
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblStudentDetail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DA.SelectCommand = cmd;
                    DA.Fill(DT);
                    dgvStudent.DataSource = DT;
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
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    if(txtFullName.Text.Length == 0 || txtPhone.Text.Length==0)
                    {
                        MessageBox.Show("One Or More Fields Are Required!");
                    }
                    else
                    {
                        sql = "INSERT INTO tblStudentDetail(Stud_ID,Full_Name,Gender,Date_Of_Birth,Department,Phone_Number) VALUES('"+ txtStudentID.Text + "','" + txtFullName.Text + "','" +
                            cboGender.Text + "','" + txtDOB.Text + "','" + cboDepartment.Text + "','" + txtPhone.Text + "')";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Student Detail Inserted Successfully!");
                            txtFullName.Clear();
                            txtPhone.Clear();
                            txtDOB.Clear();
                            cboGender.ResetText();
                            cboDepartment.ResetText();
                            sql = "SELECT * FROM tblStudentDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvStudent.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Add Student Detail");
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

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                txtStudentID.Text = dgvStudent.CurrentRow.Cells[0].Value.ToString();
                txtFullName.Text = dgvStudent.CurrentRow.Cells[1].Value.ToString();
                cboGender.Text = dgvStudent.CurrentRow.Cells[2].Value.ToString();
                txtDOB.Text = dgvStudent.CurrentRow.Cells[3].Value.ToString();
                cboDepartment.Text = dgvStudent.CurrentRow.Cells[4].Value.ToString();
                txtPhone.Text = dgvStudent.CurrentRow.Cells[5].Value.ToString();
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
                    if(txtStudentID.Text.Length == 0)
                    {
                        MessageBox.Show("To Delete Student Data, Student ID Field Is Required!");
                    }
                    else
                    {
                        sql = "DELETE FROM tblStudentDetail WHERE Stud_ID='" + txtStudentID.Text + "'";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Student Data Deleted Successfully!");
                            txtFullName.Clear();
                            txtPhone.Clear();
                            txtDOB.Clear();
                            cboGender.ResetText();
                            cboDepartment.ResetText();
                            sql = "SELECT * FROM tblStudentDetail";
                            cmd.CommandText = sql;
                            DA.SelectCommand = cmd;
                            DA.Fill(DT);
                            dgvStudent.DataSource = DT;
                        }
                        else
                        {
                            MessageBox.Show("Unable To Delete Student Data!");
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
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "UPDATE tblStudentDetail SET Full_Name='" + txtFullName.Text + "', Gender='"+cboGender.Text + "', Date_Of_Birth='" +
                        txtDOB.Text + "', Department='"+cboDepartment.Text + "', Phone_Number='" + txtPhone.Text + "' WHERE Stud_ID='" + txtStudentID.Text +"'";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Student Data Updated Successfully!");
                        txtFullName.Clear();
                        txtPhone.Clear();
                        txtDOB.Clear();
                        cboGender.ResetText();
                        cboDepartment.ResetText();
                        sql = "SELECT * FROM tblStudentDetail";
                        cmd.CommandText = sql;
                        DA.SelectCommand = cmd;
                        DA.Fill(DT);
                        dgvStudent.DataSource = DT;
                    }
                    else
                    {
                        MessageBox.Show("Unable To Update Student Data!");
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
