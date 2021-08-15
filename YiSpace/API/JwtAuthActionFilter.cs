using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Jose;
using YiSpace.Models;
using YiSpace.Models.API;

namespace YiSpace.API
{
    public class JwtAuthActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            APIResponseBaseModel<dynamic> resp = new APIResponseBaseModel<dynamic> { Success = false };
            if (actionContext.Request.Headers.Authorization == null || actionContext.Request.Headers.Authorization.Scheme != "Bearer")
            {
                resp.Message = "驗證錯誤";
                setErrorResponse(actionContext, resp);
            }
            else
            {
                try
                {
                    var jwtObject = Jose.JWT.Decode<ApiToken>(
                        actionContext.Request.Headers.Authorization.Parameter,
                        System.Text.Encoding.UTF8.GetBytes(DAL.Information.APISecret),
                        JwsAlgorithm.HS256);
                    if (Convert.ToDateTime(jwtObject.expire) < DateTime.UtcNow.AddHours(08))
                    {
                        resp.Message = "驗證過期";
                        actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden, resp);
                    }
                }
                catch (Exception ex)
                {
                    resp.Message = ex.Message;
                    setErrorResponse(actionContext, resp);
                }
            }

            base.OnActionExecuting(actionContext);
        }

        private static void setErrorResponse(HttpActionContext actionContext, APIResponseBaseModel<dynamic> Result)
        {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, Result);
            actionContext.Response = response;
        }
    }
}