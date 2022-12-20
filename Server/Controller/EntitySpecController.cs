using EntityContext.Fms;
using Extensions.Extension;
using Microsoft.AspNetCore.Mvc;
using QueryManager.Interface;
using Shared.ApiResult;

namespace blazor_wasm.Server.Controllers.Layout
{
    [ApiController]
    [Route("entity-spec")]
    public class EntitySpecController : Controller
    {
        private readonly ILogger<EntitySpecController> _logger;
        private readonly IQueryManager _queryManager;

        public EntitySpecController(ILogger<EntitySpecController> logger, IQueryManager queryManager)
        {
            _logger = logger;
            _queryManager = queryManager;
        }


        [HttpGet]
        [Route("get/entity-type-collection")]
        public HttpResult<List<EntitySpec>> GetEntityTypeCollection([FromQuery] String? entityType)
        {
            HttpResult<List<EntitySpec>> httpResult = new();
            try
            {
                if (entityType is null) throw new Exception("entityType is null");

                EntitySpec rs = new EntitySpec();
                string? qry = rs.GetReadQuery();
                qry += $" where entity_type = {entityType.Quot()}";
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<EntitySpec>(qry);

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

        [HttpGet]
        [Route("get")]
        public HttpResult<List<EntitySpec>> Get()
        {
            HttpResult<List<EntitySpec>> httpResult = new();
            try
            {
                EntitySpec rs = new EntitySpec();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<EntitySpec>(qry);

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
        public HttpResult<EntitySpec> Post(EntitySpec? request)
        {
            HttpResult<EntitySpec> httpResult = new();
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
        public HttpResult<int> Put(EntitySpec? request)
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
        public HttpResult<int> Delete([FromQuery] String? entityType, [FromQuery] String? entityItemCd, [FromQuery] String? entityItemVal)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (entityType is null || string.IsNullOrEmpty(entityType)) throw new Exception("id not found");
                if (entityItemCd is null || string.IsNullOrEmpty(entityItemCd)) throw new Exception("id not found");
                if (entityItemVal is null || string.IsNullOrEmpty(entityItemVal)) throw new Exception("id not found");

                EntitySpec item = new EntitySpec();
                item.EntityType = entityType;
                item.EntityItemCd = entityItemCd;
                item.EntityItemVal = entityItemVal;
                string? qry = item.GetDeleteQuery();
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
