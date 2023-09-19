using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Library_Management_System
{
    class DBConnection
    {
        //in this class file we will create database connection with our database then we will call it when it is necessary
        private SqlConnection conn = new SqlConnection("Data source=ABEL; Initial catalog=Library_Management_System; Integrated security=true;");

        //function that opens connection with database
        public SqlConnection connection()
        {
            return conn;
        }
    }
}
