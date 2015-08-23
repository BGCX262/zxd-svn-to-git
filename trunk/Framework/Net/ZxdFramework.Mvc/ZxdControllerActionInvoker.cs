using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ZxdFramework.Json;
using ZxdFramework.Mvc.Controllers;
using ZxdFramework.Mvc.Services;
using ZxdFramework.Service;
using System.Linq;

namespace ZxdFramework.Mvc
{
    /// <summary>
    /// 执行控制器对象
    /// </summary>
    public class ZxdControllerActionInvoker : ControllerActionInvoker
    {

        private bool _isJsonRequest = false;
        private IJsonRequest _jsonRequest;

        /// <summary>
        /// Invokes the specified action by using the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionName">The name of the action to invoke.</param>
        /// <returns>The result of executing the action.</returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="controllerContext"/> parameter is null.</exception>
        /// <exception cref="T:System.ArgumentException">The <paramref name="actionName"/> parameter is null or empty.</exception>
        /// <exception cref="T:System.Threading.ThreadAbortException">The thread was aborted during invocation of the action.</exception>
        /// <exception cref="T:System.Exception">An unspecified error occurred during invocation of the action.</exception>
        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {

            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException("方法名不能为空", "actionName");
            }

            var controllerDescriptor = GetControllerDescriptor(controllerContext);
            var actionDescriptor = FindAction(controllerContext, controllerDescriptor, actionName);

            if (actionDescriptor != null)
            {
                var filterInfo = GetFilters(controllerContext, actionDescriptor);

                try
                {
                    //检查是否具有Json请求对象
                    _isJsonRequest = JsonRequest.CheckJsonRequest(controllerContext.HttpContext.Request);
                    if (_isJsonRequest)
                    {
                        _jsonRequest = JsonRequest.GetJsonRequest(controllerContext.HttpContext.Request);
                    }
                    //如果是支持Json请求的控制器. 传入请求对象
                    if (controllerContext.Controller is IJsonControllerProvider)
                    {
                        var ctr = (IJsonControllerProvider) controllerContext.Controller;
                        ctr.JsonRequest = _jsonRequest;
                    }

                    if (controllerContext.Controller is ZxdFrameworkController)
                    {
                        ((ZxdFrameworkController) controllerContext.Controller).CurrentUser =
                            ServiceFactory.AuthorityService.CurrentUser;
                    }


                    var authContext = InvokeAuthorizationFilters(controllerContext, filterInfo.AuthorizationFilters, actionDescriptor);
                    if (authContext.Result != null)
                    {
                        // the auth filter signaled that we should let it short-circuit the request
                        InvokeActionResult(controllerContext, authContext.Result);
                    }
                    else
                    {
                        if (controllerContext.Controller.ValidateRequest)
                        {
                            ValidateRequest(controllerContext);
                        }
                        //获取需要传入的参数
                        var parameters = GetParameterValues(controllerContext, actionDescriptor);
                        //执行方法和
                        var postActionContext = InvokeActionMethodWithFilters(controllerContext, filterInfo.ActionFilters, actionDescriptor, parameters);
                        InvokeActionResultWithFilters(controllerContext, filterInfo.ResultFilters, postActionContext.Result);
                    }
                }
                catch (ThreadAbortException)
                {
                    // This type of exception occurs as a result of Response.Redirect(), but we special-case so that
                    // the filters don't see this as an error.
                    throw;
                }
                catch (Exception ex)
                {
                    // something blew up, so execute the exception filters
                    ExceptionContext exceptionContext = InvokeExceptionFilters(controllerContext, filterInfo.ExceptionFilters, ex);
                    if (!exceptionContext.ExceptionHandled)
                    {
                        throw;
                    }
                    InvokeActionResult(controllerContext, exceptionContext.Result);
                }

                return true;
            }

            // notify controller that no method matched
            return false;

        }


        protected override IDictionary<string, object> GetParameterValues(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var parametersDict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            var parameterDescriptors = actionDescriptor.GetParameters();

            if (_isJsonRequest)
            {
                //执行Json的数据拼装
                if (parameterDescriptors.Length == 1)
                    parametersDict[parameterDescriptors[0].ParameterName] = GetJsonParameterValue(controllerContext, parameterDescriptors[0]);
                else
                {
                    foreach (var parameterDescriptor in parameterDescriptors)
                    {
                        //parametersDict[parameterDescriptor.ParameterName] = GetParameterValue(controllerContext, parameterDescriptor);
                        parametersDict[parameterDescriptor.ParameterName] = GetJsonDictValue(controllerContext,
                                                                                             parameterDescriptor);
                    }
                }

            }
            else
            {
                foreach (var parameterDescriptor in parameterDescriptors)
                {
                    parametersDict[parameterDescriptor.ParameterName] = GetParameterValue(controllerContext, parameterDescriptor);
                }
            }

            return parametersDict;
        }


        protected virtual object GetJsonParameterValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
        {
            return _jsonRequest.GetPostData(parameterDescriptor.ParameterType);
        }

        protected virtual object GetJsonDictValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
        {
            var temp = _jsonRequest.Paramters[parameterDescriptor.ParameterName];
            return JsonConverter.DeserializeObject(parameterDescriptor.ParameterType, temp.ToString());
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
        {

            if (actionReturnValue is ActionResult) return (ActionResult) actionReturnValue;

            if (_isJsonRequest)
            {
                //执行Json 的返回格式
                IJsonResponse response = null;
                if (actionReturnValue is IJsonResponse)
                {
                    response = (IJsonResponse) actionReturnValue;
                } 
                else if(controllerContext.Controller is IJsonControllerProvider)
                {
                    response = ((IJsonControllerProvider) controllerContext.Controller).JsonResponse;

                    if(actionReturnValue != null)
                    response.JsonData = JsonConverter.Serialize(actionReturnValue);
                }
                else
                {
                    response = new JsonResponse();
                }
                return new JsonResult(response);

            }


            return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
        }

        internal static void ValidateRequest(ControllerContext controllerContext)
        {
            if (controllerContext.IsChildAction)
            {
                return;
            }

            // DevDiv 214040: Enable Request Validation by default for all controller requests
            // 
            // Earlier versions of this method dereferenced Request.RawUrl to force validation of
            // that field. This was necessary for Routing before ASP.NET v4, which read the incoming
            // path from RawUrl. Request validation has been moved earlier in the pipeline by default and
            // routing no longer consumes this property, so we don't have to either.

            controllerContext.HttpContext.Request.ValidateInput();
        }
    }
}