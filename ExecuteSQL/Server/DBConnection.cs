using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
///DBConnection 的摘要说明
/// </summary>
/// 
namespace ExecuteSQL.Server
{
    public class DBConnection
    {
        //public string connectionStr = "Data Source=.;Initial Catalog=Skyland.LIMS.FS;User ID=sa;Password=123456;Pooling=true;Max Pool Size = 5120;Min Pool Size=0";
        SqlConnection myConn;
        SqlTransaction transaction;//事务
        SqlCommand transactionCommand;
        public DBConnection()
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            myConn = new SqlConnection(connectionStr);
        }
        public void open()
        {
            myConn.Open();
        }
        public void close()
        {
            if (myConn != null)
            {
                myConn.Close();
            }
        }

        public bool TransactionStart()
        {
            myConn.Open();
            transaction = myConn.BeginTransaction();
            transactionCommand = myConn.CreateCommand();
            transactionCommand.Transaction = transaction;
            return true;
        }
        public bool TransactionExecuteSql(string sqlStr)
        {
            try
            {
                transactionCommand.CommandText = sqlStr;
                transactionCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public int TransactionExecuteSqlInt(string sqlStr)
        {
            int num = 0;
            try
            {
                transactionCommand.CommandText = sqlStr;
                num = transactionCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return num;
        }
        public bool TransactionCommit()
        {
            try
            {
                transaction.Commit();
                myConn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                myConn.Close();
                throw ex;
            }
            return true;
        }
        public bool TransactionRollback()
        {
            try
            {
                transaction.Rollback();
                myConn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                myConn.Close();
                throw ex;
            }
            return true;
        }
        public bool ExecuteNonQueryDb(string sqlStr)
        {
            myConn.Open();
            SqlTransaction tptransaction = myConn.BeginTransaction();
            SqlCommand tpcommand = myConn.CreateCommand();
            tpcommand.Transaction = tptransaction;
            tpcommand.CommandText = sqlStr;
            try
            {
                tpcommand.ExecuteNonQuery();
                tptransaction.Commit();
                myConn.Close();
                // myConn.Dispose();
            }
            catch (Exception ex)
            {
                tptransaction.Rollback();
                myConn.Close();
                //  myConn.Dispose();
                throw ex;
            }
            return true;
        }
        public bool UptateDb(string sqlStr)
        {
            return ExecuteNonQueryDb(sqlStr);
        }
        public bool InsertDb(string sqlStr)
        {
            return ExecuteNonQueryDb(sqlStr);
        }
        public bool DeleteDb(string sqlStr)
        {
            return ExecuteNonQueryDb(sqlStr);
        }
        public DataTable SelectDb(string sqlStr, int begin, int end)
        {
            SqlDataReader reader = null;
            DataTable table = new DataTable();
            myConn.Open();
            SqlCommand command = myConn.CreateCommand();
            command.CommandText = sqlStr;
            reader = command.ExecuteReader();
            string colname = string.Empty;
            //System.Type type;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                colname = reader.GetName(i);
                table.Columns.Add(colname);
            }
            DataRow dataRow;
            int count = -1;
            while (reader.Read())
            {
                count++;
                if (begin != -1)
                {
                    if (begin > count)
                    {
                        continue;
                    }
                }
                dataRow = table.NewRow();//以table的格式新建一个空行
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader[i];
                }
                table.Rows.Add(dataRow);
                if (end != -1)
                {
                    if (end < count)
                    {
                        break;
                    }
                }
            }
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
            myConn.Close();
            // myConn.Dispose();
            return table;
        }
        public DataTable TransactionSelect(string sqlStr, int begin, int end)
        {
            SqlDataReader reader = null;
            DataTable table = new DataTable();
            SqlCommand tempCommand;
            //  myConn.Open();
            tempCommand = transactionCommand;
            tempCommand.CommandText = sqlStr;
            reader = tempCommand.ExecuteReader();
            string colname = string.Empty;
            for (int i = 0; i < reader.FieldCount; i++)
            {
                colname = reader.GetName(i);
                table.Columns.Add(colname);
            }
            DataRow dataRow;
            int count = -1;
            while (reader.Read())
            {
                count++;
                if (begin != -1)
                {
                    if (begin > count)
                    {
                        continue;
                    }
                }
                dataRow = table.NewRow();//以table的格式新建一个空行
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader[i];
                }
                table.Rows.Add(dataRow);
                if (end != -1)
                {
                    if (end < count)
                    {
                        break;
                    }
                }
            }
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
            // myConn.Close();
            return table;
        }
    }
}