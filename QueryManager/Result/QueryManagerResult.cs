using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryManager.Result
{
    public class QueryManagerResult<T> where T : new()
    {
        public List<T> Values { get; set; }
        public int ResultCode { get; set; }
        public int ExcuteCnt { get; set; }
        public string Message { get; set; }

        public QueryManagerResult()
        {
            Values = new List<T>();
            ResultCode = -1;
            Message = "";
        }

        public bool GetIsSucceed()
        {
            if (ResultCode < 0) return false;
            return true;
        }

        public void SetOk(List<T> ts, int resultCode = 0)
        {
            Values = ts;
            ResultCode = resultCode;
        }
        public void SetOk(int resultCode = 0)
        {
            ResultCode = resultCode;
        }
        public void SetFail(string message = "", int resultCode = -1)
        {
            ResultCode = resultCode;
        }
    }
}
