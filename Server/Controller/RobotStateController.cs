using EntityContext.Fms;
using Microsoft.AspNetCore.Mvc;
using QueryManager.Interface;
using Shared.ApiResult;
using System.Text.Json;

namespace blazor_wasm.Server.Controllers.Layout
{
    /*
    * get resultJson = List<T>
    * post resultJson = T
    */

    [ApiController]
    [Route("robot-state")]
    public class RobotStateController : Controller
    {
        private readonly ILogger<RobotStateController> _logger;
        private readonly IQueryManager _queryManager;

        public RobotStateController(ILogger<RobotStateController> logger, IQueryManager queryManager)
        {
            _logger = logger;
            _queryManager = queryManager;
        }

        [HttpGet]
        [Route("get")]
        public HttpResult<List<RobotState>> Get()
        {
            HttpResult<List<RobotState>> httpResult = new();
            try
            {
                RobotState rs = new RobotState();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<RobotState>(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.Values;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = null;
            }
            return httpResult;
        }

        [HttpPost]
        [Route("post")]
        public HttpResult<RobotState> Post(RobotState? request)
        {
            HttpResult<RobotState> httpResult = new();
            try
            {
                if (request is null)
                {
                    throw new Exception("a request item is null");
                }

                string? qry = request.GetCreateQuery();
                httpResult.Message = qry;
                if (qry is null)
                {
                    throw new Exception("exception model GetCreateQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = request;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = null;
            }
            return httpResult;
        }


        [HttpPut]
        [Route("put")]
        public HttpResult<int> Put(RobotState? request)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (request is null)
                {
                    throw new Exception("a request item is null");
                }

                string? qry = request.GetUpdateQuery();
                httpResult.Message = qry;
                if (qry is null)
                {
                    throw new Exception("exception model GetCreateQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.ExcuteCnt;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = -1;
            }
            return httpResult;
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResult<int> Delete([FromQuery] String? robotId)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if(robotId is null) throw new Exception("id not found");
                
                RobotState rs = new RobotState();
                rs.RobotId = robotId;
                string? qry = rs.GetDeleteQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetDeleteQuery()");
                }
                var result = _queryManager.ExcuteNonQuery(qry);

                httpResult.Success = result.GetIsSucceed();
                httpResult.Result = result.ExcuteCnt;
                if (httpResult.Success == false)
                {
                    httpResult.Message = qry;
                }
            }
            catch (System.Exception exception)
            {
                httpResult.Success = false;
                httpResult.Message = exception.Message;
                httpResult.Result = -1;
            }
            return httpResult;
        }
    }
}
