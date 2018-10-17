using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace InterfaceSQL
{
    public class SQLiteMethod : IMethod
    {
        string conStr = string.Empty;

        public SQLiteMethod()
        {

        }

        public override bool InitSQL(string configPath)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            ReadConfig.GetConfigInfoFromFile(configPath, dictionary);

            foreach (var item in dictionary)
            {
                conStr += item.Key + "=" + item.Value + ";";
            }
            return true;
        }

        public override bool InsertData(string sql, params DbParameter[] parameter)
        {
            return CUDData(sql, parameter);
        }

        public override bool UpdateData(string sql, params DbParameter[] parameter)
        {
            return CUDData(sql, parameter);
        }

        public override bool DeleteData(string sql, params DbParameter[] parameter)
        {
            return CUDData(sql, parameter);
        }

        public override object GetData(string sql, params DbParameter[] parameter)
        {
            DataTable table = new DataTable();
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, con))
                {
                    if (parameter != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parameter);
                    }
                    adapter.Fill(table);
                }
            }
            return table;
        }

        public override object GetScalar(string sql, params DbParameter[] parameter)
        {
            object obj = null;
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (parameter != null)
                    {
                        cmd.Parameters.AddRange(parameter);
                    }
                    obj = cmd.ExecuteScalar();
                }
            }
            return obj;
        }

        public override string GetStringByColumn(string sql, string column, params DbParameter[] parameter)
        {
            string value = string.Empty;
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if (parameter != null)
                    {
                        cmd.Parameters.AddRange(parameter);
                    }

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            value = reader[column].ToString();
                        }
                    }
                }
            }
            return value;
        }

        public override List<string> GetListByColumn(string sql, string column, params DbParameter[] parameter)
        {
            List<string> list = new List<string>();
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if(con.State ==  ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if(parameter != null)
                    {
                        cmd.Parameters.AddRange(parameter);
                    }
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            list.Add(reader[column].ToString());
                        }
                    }
                }
                
            }
            return list;
        }

        protected override bool CUDData(string sql, params DbParameter[] parameter)
        {
            bool ret = false;
            int count = -1;
            using (SQLiteConnection con = new SQLiteConnection(conStr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    if(con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    if(parameter != null)
                    {
                        cmd.Parameters.AddRange(parameter);
                    }
                    count = cmd.ExecuteNonQuery();
                    if(count >= 0)
                    {
                        ret = true;
                    }

                }
            }
            return ret;
        }
    }
}
