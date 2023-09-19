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
    public partial class frmAccount : Form
    {
        private DBConnection conn = new DBConnection();
        private SqlCommand cmd = new SqlCommand();
        private string sql;
        public frmAccount()
        {
            InitializeComponent();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void chkShowConfPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowConfPassword.Checked)
            {
                txtConfPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtConfPassword.UseSystemPasswordChar = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlDataReader DR;
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    if(txtUserNameSearch.Text.Length == 0)
                    {
                        MessageBox.Show("User Name Is Required To Retrive Your Data!");
                        txtUserNameSearch.Focus();
                    }
                    else
                    {
                        sql = "SELECT * FROM tblStaff WHERE User_Name='" + txtUserNameSearch.Text + "'";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        DR = cmd.ExecuteReader();
                        if (!DR.HasRows)
                        {
                            MessageBox.Show("Your data is not found please check your user name!");
                        }
                        else
                        {
                            while (DR.Read())
                            {
                                txtID.Text = DR.GetValue(0).ToString();
                                txtFullName.Text = DR.GetValue(1).ToString();
                                txtUserName.Text = DR.GetValue(2).ToString();
                                txtPassword.Text = DR.GetValue(3).ToString();
                                txtConfPassword.Text = DR.GetValue(3).ToString();
                                cboDesignation.Text = DR.GetValue(5).ToString();
                            }
                        }
                        DR.Close();
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

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            frmPublisher publisher = new frmPublisher();
            publisher.Show();
            this.Dispose();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    if (txtUserName.Text.Length == 0 || txtFullName.Text.Length == 0 || txtPassword.Text.Length == 0 || txtConfPassword.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Can Not Be Empty");
                    }
                    else if (txtPassword.Text.Length < 6 || txtConfPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Password Length Must Be At Least 6 Characters!");
                    }
                    else if (txtPassword.Text != txtConfPassword.Text)
                    {
                        MessageBox.Show("Password Fields Did Not Mathc!");
                    }
                    else
                    {
                        sql = "UPDATE tblStaff SET FullName='" + txtFullName.Text + "', User_Name='" + txtUserName.Text + "', SPassword='" + txtPassword.Text + "', " +
                            "Designation='" + cboDesignation.Text + "' WHERE Staff_Memeber_ID=" + int.Parse(txtID.Text);
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Your Account Updated Successfully!");
                            txtID.Clear();
                            txtFullName.Clear();
                            txtUserName.Clear();
                            txtUserNameSearch.Clear();
                            txtPassword.Clear();
                            txtConfPassword.Clear();
                            cboDesignation.ResetText();
                            txtUserNameSearch.Focus();
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
    }
}
