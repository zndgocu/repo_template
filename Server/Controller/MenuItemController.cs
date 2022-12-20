using EntityContext.Fms;
using Microsoft.AspNetCore.Mvc;
using QueryManager.Interface;
using Shared.ApiResult;

namespace blazor_wasm.Server.Controllers.Layout
{
    [ApiController]
    [Route("menu-item")]
    public class MenuItemItemController : Controller
    {
        private readonly ILogger<MenuItemItemController> _logger;
        private readonly IQueryManager _queryManager;

        public MenuItemItemController(ILogger<MenuItemItemController> logger, IQueryManager queryManager)
        {
            _logger = logger;
            _queryManager = queryManager;
        }


        [HttpGet]
        [Route("get")]
        public HttpResult<List<MenuItem>> Get()
        {
            HttpResult<List<MenuItem>> httpResult = new();
            try
            {
                MenuItem rs = new MenuItem();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<MenuItem>(qry);

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
        public HttpResult<MenuItem> Post(MenuItem? request)
        {
            HttpResult<MenuItem> httpResult = new();
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
        public HttpResult<int> Put(MenuItem? request)
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
                httpResult.Message = qry;
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
        public HttpResult<int> Delete([FromQuery] String? id)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (id is null) throw new Exception("id not found");

                MenuItem mi = new MenuItem();
                mi.Id = id;
                string? qry = mi.GetDeleteQuery();
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
