using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Webstudent.Repository
{
    public class dbRepository
    {
        //SqlConnection connServer = new SqlConnection("Server=fondb; Initial Catalog=FON_CarReserv; User ID=xxxx; Password=xxxx");
        SqlConnection connServer = new SqlConnection("Server=localhost\\SQLEXPRESS; database=DB_SD_Demo1; integrated security = SSPI;");
        DataTable dt = new DataTable();

        public DataSet getAllStudent()
        {
            connServer.Open();
            string Sql = "select * from Student_Demo1";
            SqlCommand cmd = new SqlCommand(Sql, connServer);
            cmd.CommandType = CommandType.Text;
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);
            connServer.Close();
            return ds;
        }

        public DataSet getEditData(int id)
        {
            connServer.Open();
            string Sql = "select * from Student_Demo1 where ID="+id+"";
            SqlCommand cmd = new SqlCommand(Sql, connServer);
            cmd.CommandType = CommandType.Text;
            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            da.Fill(ds);
            connServer.Close();
            return ds;
        }
        public void UpdateStudent(int Id, string Name, string Birth, string Address, string Tel, string Date_start, string Date_End)
        {
            connServer.Open();
            SqlCommand command = connServer.CreateCommand();
            SqlTransaction transaction;
            transaction = connServer.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                command.CommandText = "update Student_Demo1 set Name = '"+ Name + "',Birth = '" + Birth + "',Address = '" + Address + "',Tel = '" + Tel + "',Date_star = '" + Date_start + "',Date_End = '" + Date_End + "' where ID=" + Id + "";
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Sucess");

            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error");
            }
            connServer.Close();

        }

        public void inserStudent(string Name, string Birth, string Address, string Tel, string Date_start, string Date_End)
        {
            connServer.Open();
            SqlCommand command = connServer.CreateCommand();
            SqlTransaction transaction;
            transaction = connServer.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                command.CommandText = "insert into Student_Demo1 values ( '" + Name + "','" + Birth + "','" + Address + "','" + Tel + "','" + Date_start + "','" + Date_End + "')";
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Sucess");

            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error");
            }
            connServer.Close();

        }
         public void deleteStudent(int id)
        {
            connServer.Open();
            SqlCommand command = connServer.CreateCommand();
            SqlTransaction transaction;
            transaction = connServer.BeginTransaction(IsolationLevel.ReadCommitted);
            command.Transaction = transaction;

            try
            {
                command.CommandText = "delete Student_Demo1 where ID = " + id + "";
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Sucess");

            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error");
            }
            connServer.Close();

        }
    }
}