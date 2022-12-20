using QueryManager.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryManager.Interface
{
    public interface IQueryManager
    {
        public string GetConnectionString();
        public int GetTimeOut();
        public QueryManagerResult<T> ExcuteQuery<T>(string qry) where T : class, new();
        public QueryManagerResult<int> ExcuteNonQuery(string qry);

    }
}
