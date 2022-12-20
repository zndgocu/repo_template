using EntityContext.Fms;
using Microsoft.AspNetCore.Mvc;
using QueryManager.Interface;
using Extensions.Extension;
using Shared.ApiResult;
using blazor_wasm.Client.Shared;

namespace blazor_wasm.Server.Controllers.Layout
{
    [ApiController]
    [Route("template-page-layout")]
    public class TemplatePageLayoutController : Controller
    {
        private readonly ILogger<TemplatePageLayoutController> _logger;
        private readonly IQueryManager _queryManager;

        public TemplatePageLayoutController(ILogger<TemplatePageLayoutController> logger, IQueryManager queryManager)
        {
            _logger = logger;
            _queryManager = queryManager;
        }


        [HttpGet]
        [Route("get")]
        public HttpResult<List<TemplatePageLayout>> Get()
        {
            HttpResult<List<TemplatePageLayout>> httpResult = new();
            try
            {
                TemplatePageLayout rs = new TemplatePageLayout();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                var result = _queryManager.ExcuteQuery<TemplatePageLayout>(qry);

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
        [Route("get/{templateId}")]
        public HttpResult<List<TemplatePageLayout>> Get(string templateId)
        {
            HttpResult<List<TemplatePageLayout>> httpResult = new();
            try
            {
                TemplatePageLayout rs = new TemplatePageLayout();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                qry = qry +
                  $"  where id = {templateId.Quot()} ";

                var result = _queryManager.ExcuteQuery<TemplatePageLayout>(qry);

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
        [Route("get/parent-id/{parentId}")]
        public HttpResult<List<TemplatePageLayout>> GetChild(string parentId)
        {
            HttpResult<List<TemplatePageLayout>> httpResult = new();
            try
            {
                TemplatePageLayout rs = new TemplatePageLayout();
                string? qry = rs.GetReadQuery();
                if (qry is null)
                {
                    throw new Exception("exception model.GetReadQuery()");
                }
                qry = qry +
                    $"  where parent_id = {parentId.Quot()} ";
                var result = _queryManager.ExcuteQuery<TemplatePageLayout>(qry);

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
        public HttpResult<TemplatePageLayout> Post(TemplatePageLayout? request)
        {
            HttpResult<TemplatePageLayout> httpResult = new();
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
        public HttpResult<int> Put(TemplatePageLayout? request)
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
        public HttpResult<int> Delete([FromQuery] String? id)
        {
            HttpResult<int> httpResult = new();
            try
            {
                if (id is null) throw new Exception("id not found");

                TemplatePageLayout tpl = new TemplatePageLayout();
                tpl.Id = id;
                string? qry = tpl.GetDeleteQuery();
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
