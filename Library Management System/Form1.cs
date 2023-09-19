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
    public partial class Form1 : Form
    {
        private string sql;
        private SqlCommand cmd = new SqlCommand();
        //crate object with our database connection class
        DBConnection conn = new DBConnection();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader DR;
            try
            {
                //Open Database connection
                conn.connection().Open();
                //check connection state
                if(conn.connection().State == ConnectionState.Open)
                {
                    string user_name = txtUserName.Text;
                    string password = txtPassword.Text;
                    int counter = 0;
                    //check if the user provide neccessary inputs
                    if(user_name.Length == 0 || password.Length == 0)
                    {
                        MessageBox.Show("One Or More Fields Are Required!");
                    }
                    else
                    {
                        sql = "SELECT User_Name,SPassword FROM tblStaff WHERE User_Name='" + user_name + "' AND SPassword='" + password + "'";
                        cmd.Connection = conn.connection();
                        cmd.CommandText = sql;
                        DR = cmd.ExecuteReader();
                        while(DR.Read())
                        {
                            counter++;
                        }
                        DR.Close();
                        if(counter == 1)
                        {
                            MessageBox.Show("Logged In Successfully!");
                            frmDashBoard home = new frmDashBoard();
                            home.Show();
                            this.Hide();
                            
                        }
                        else
                        {
                            MessageBox.Show("Invalid Creditionals, Check Your User Name And Password!");
                        }
                    }
                }
            }
            //this catch exception will be excuted if we have error with our database operations
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //this catch exception will be executed if we have general errors with our operation/program
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //Finally close the connection with database
            finally
            {
                conn.connection().Close();
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            frmSignup signup = new frmSignup();
            signup.Show();
            this.Hide();
        }
    }
}
