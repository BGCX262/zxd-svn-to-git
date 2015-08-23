using System;
using System.Web.Mvc;
using ZxdFramework.DataContract;
using ZxdFramework.Mvc.Filters;
using ZxdFramework.Mvc.Services;
using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Controllers
{
    /// <summary>
    /// ����ڵĿ������Ļ�������
    /// </summary>
    [SessionView, JsonError, PageError]
    public abstract class ZxdFrameworkController : Controller, IJsonControllerProvider
    {
        protected ZxdFrameworkController()
        {
            JsonResponse = new JsonResponse();
        }

        /// <summary>
        /// ��ȡJson��������ݶ���. �����ǰ��������Json��ôΪ Null
        /// </summary>
        /// <value>The json request.</value>
        public IJsonRequest JsonRequest { set; get; }

        public IJsonResponse JsonResponse { set; get; }


        /// <summary>
        /// ��ȡ��������ִ�ж���
        /// </summary>
        /// <returns>An action invoker.</returns>
        protected override IActionInvoker CreateActionInvoker()
        {
            return new ZxdControllerActionInvoker();
        }

        /// <summary>
        /// ��ȡ��ǰ���û�
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public IUser CurrentUser { set; get; }
    }

}