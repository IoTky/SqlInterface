using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace InterfaceSQL
{
    public abstract class IMethod
    {
        public abstract bool InitSQL(string filePath);
        protected abstract bool CUDData(string sql, params DbParameter[] parameter);
        public abstract bool InsertData(string sql,params DbParameter[] parameter);
        public abstract bool DeleteData(string sql, params DbParameter[] parameter);
        public abstract bool UpdateData(string sql, params DbParameter[] parameter);
        public abstract object GetData(string sql, params DbParameter[] parameter);
        public abstract object GetScalar(string sql, params DbParameter[] parameter);
        public abstract string GetStringByColumn(string sql, string column, params DbParameter[] parameter);
        public abstract List<string> GetListByColumn(string sql, string column, params DbParameter[] parameter);
    }
}
