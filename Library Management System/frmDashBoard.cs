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
    public partial class frmDashBoard : Form
    {
        SqlCommand cmd = new SqlCommand();
        string sql;
        DBConnection conn = new DBConnection();
        public frmDashBoard()
        {
            InitializeComponent();
        }

        private int totalStudent()
        {
            SqlDataReader DR;
            int counter = 0;
            try
            {
                conn.connection().Open();
                if(conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblStudentDetail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while(DR.Read())
                    {
                        counter++;
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
            return counter;
        }

        private int totalCategory()
        {
            SqlDataReader DR;
            int counter = 0;
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblCategory";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        counter++;
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
            return counter;
        }

        private int totalPublisher()
        {
            SqlDataReader DR;
            int counter = 0;
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
                        counter++;
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
            return counter;
        }

        private int totalBook()
        {
            SqlDataReader DR;
            int counter = 0;
            try
            {
                conn.connection().Open();
                if (conn.connection().State == ConnectionState.Open)
                {
                    sql = "SELECT * FROM tblBookDetail";
                    cmd.Connection = conn.connection();
                    cmd.CommandText = sql;
                    DR = cmd.ExecuteReader();
                    while (DR.Read())
                    {
                        counter++;
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
            return counter;
        }
        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            lblTotalStudent.Text += totalStudent();
            lblTotalBooks.Text += totalBook();
            lblTotalCategory.Text += totalCategory();
            lblTotalPublisher.Text += totalPublisher();
            btnHome.Enabled = false;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
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

        private void btnAccount_Click(object sender, EventArgs e)
        {
            frmAccount account = new frmAccount();
            account.Show();
            this.Dispose();
        }
    }
}
