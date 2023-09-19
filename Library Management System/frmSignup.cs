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
    public partial class frmSignup : Form
    {
        private DBConnection conn = new DBConnection();
        private SqlCommand cmd = new SqlCommand();
        string sql;
        public frmSignup()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Dispose();
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

        private void btnSignup_Click(object sender, EventArgs e)
        {
            int isAdmin=0;
            if(cboUserType.Text == "Admin")
            {
                isAdmin = 1;
            }
            else
            {
                isAdmin = 0;
            }
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    if(txtUserName.Text.Length == 0 || txtFullName.Text.Length== 0 || txtPassword.Text.Length==0 || txtConfPassword.Text.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required!");
                    }
                    else if(txtPassword.Text.Length < 6 || txtConfPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Password Length Must Be Equal To Or Greater Than 6");
                    }
                    else if(txtPassword.Text != txtConfPassword.Text)
                    {
                        MessageBox.Show("Password Fields Did Not Match!");
                    }
                    else
                    {
                        sql = "INSERT INTO tblStaff(FullName,User_Name,SPassword,is_Admin,Designation) VALUES('"+txtFullName.Text+"','"
                            +txtUserName.Text+"','"+txtPassword.Text+"',"+isAdmin+",'"+cboDesignation.Text+"')";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        if(cmd.ExecuteNonQuery()>0)
                        {
                            MessageBox.Show("User Account Created Successfully!");
                            txtFullName.Clear();
                            txtUserName.Clear();
                            txtPassword.Clear();
                            txtConfPassword.Clear();
                            cboDesignation.ResetText();
                            cboUserType.ResetText();
                        }
                        else
                        {
                            MessageBox.Show("Unable To create User Account!");
                        }
                    }
                }
            }
            catch (SqlException ex)
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
