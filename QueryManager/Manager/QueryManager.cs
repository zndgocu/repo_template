using EntityHelper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using QueryManager.Interface;
using QueryManager.Result;
using System.Data;
using System.Data.Common;

namespace QueryManager.Manager
{
    public class QueryManager : IQueryManager
    {

        protected IConfiguration Config;
        public QueryManager(IConfiguration config)
        {
            Config = config;
        }

        public string GetConnectionString()
        {
            string? useDbPropName = Config.GetConnectionString("UseDb");
            if (useDbPropName is null) useDbPropName = "UseDb";
            string? connectionString = Config.GetConnectionString(useDbPropName);
            return connectionString ?? "";
        }


        public int GetTimeOut()
        {
            return Convert.ToInt32(Config.GetSection("DataBaseProperties").GetSection("TimeOut").Value);
        }

        public QueryManagerResult<T> ExcuteQuery<T>(string qry) where T : class, new()
        {
            QueryManagerResult<T> result = new QueryManagerResult<T>();
            try
            {
                DataTable dt = new DataTable();
                string connectionString = GetConnectionString();

                using (NpgsqlConnection cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, cn))
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = qry;
                        cmd.CommandTimeout = GetTimeOut();
                        DbDataReader dr;
                        dr = cmd.ExecuteReader();
                        dt.Load(dr);
                    }
                }
                result.SetOk(EntityConverter.GetEntites<T>(dt, true));
            }
            catch (Exception exAll)
            {
                result.SetFail(exAll.Message);
            }
            return result;
        }


        public QueryManagerResult<int> ExcuteNonQuery(string qry)
        {
            QueryManagerResult<int> result = new QueryManagerResult<int>();
            try
            {
                string connectionString = GetConnectionString();

                using (NpgsqlConnection cn = new NpgsqlConnection(connectionString))
                {
                    cn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(qry, cn))
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = qry;
                        cmd.CommandTimeout = GetTimeOut();
                        using (NpgsqlTransaction trn = cn.BeginTransaction())
                        {
                            try
                            {
                                result.ExcuteCnt = cmd.ExecuteNonQuery();
                                if (result.ExcuteCnt < 0)
                                {
                                    throw new Exception("result count is -1");
                                }
                                trn.Commit();
                            }
                            catch (System.Exception exception)
                            {
                                result.ResultCode = -1;
                                trn.Rollback();
                                throw new Exception(exception.Message);
                            }
                        }
                    }
                }
                result.SetOk(result.ExcuteCnt);
            }
            catch (Exception exAll)
            {
                result.SetFail(exAll.Message);
            }
            return result;
        }
    }
}
