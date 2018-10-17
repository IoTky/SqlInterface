using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceSQL;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data.SQLite;
namespace _01_调用库
{
    class Program
    {
        static void InvokeFunc(AbstractSQL abstractSQL, String sql,params MySqlParameter[] parameter)
        {
            IMethod method = abstractSQL.CreateSQL();
            method.InitSQL("./MysqlConfig.ini");
            Console.WriteLine( method.GetStringByColumn(sql,"uName",parameter));
            
        }

        static void InvokeFuncSqlite(AbstractSQL abstractSQL, String sql, params SQLiteParameter[] parameter)
        {
            IMethod method = abstractSQL.CreateSQL();
            method.InitSQL("./SqliteConfig.ini");
            Console.WriteLine(method.GetStringByColumn(sql, "au_name", parameter));

        }
          
        
        static void InvokeFuncSqlServer(AbstractSQL abstractSQL, String sql, params SqlParameter[] parameter)
        {
            IMethod method = abstractSQL.CreateSQL();
            method.InitSQL("./SqlServerConfig.ini");
            Console.WriteLine(method.GetStringByColumn(sql, "c_name", parameter));

        }


        static void Main(string[] args)
        {
            MySQL mysql = new MySQL();
            AbstractSQL sqlite = new SQLite();
            AbstractSQL sqlServer = new SQLServer();

            //sqlite调用
            SQLiteParameter parameter2 = new SQLiteParameter("@id", 60);
            InvokeFuncSqlite(sqlite, "select au_name from CarInfo where id=@id",parameter2);

            //mysql调用
            MySqlParameter parameter = new MySqlParameter("@id", 1);
            InvokeFunc(mysql,"select * from User where uId=@id", parameter);

            //SQL server调用
            SqlParameter parameter1 = new SqlParameter("@date", "20160614");
            InvokeFuncSqlServer(sqlServer, "select c_name from set_sys_date where c_date=@date",parameter1);



            Console.ReadKey();
        }
    }
}
