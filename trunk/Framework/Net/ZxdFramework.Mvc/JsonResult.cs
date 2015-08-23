using System;
using System.Web.Mvc;
using ZxdFramework.Json;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonResult : ActionResult
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResult"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public JsonResult(IJsonResponse response)
        {
            Response = response;
        }

        /// <summary>
        /// ���û�ȡ���ض���. 
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public IJsonResponse Response { set; get; }


        public override void ExecuteResult(ControllerContext context)
        {
            var httpResponse = context.HttpContext.Response;
            httpResponse.ContentType = "application/json;charset=UTF-8";
            httpResponse.AddHeader(JsonResponse.ResponseHeader, Response.ToString());
            httpResponse.Write(Response.JsonData);
        }
    }
}