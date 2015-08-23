using ZxdFramework.Service;

namespace ZxdFramework.Mvc.Controllers
{
    /// <summary>
    /// ����֧�� Json����Ŀ�����
    /// </summary>
    public interface IJsonControllerProvider
    {
        /// <summary>
        /// ��ȡJson��������ݶ���. �����ǰ��������Json��ôΪ Null
        /// </summary>
        /// <value>
        /// The json request.
        /// </value>
        IJsonRequest JsonRequest { set; get; }

        /// <summary>
        /// ����һ��Json����
        /// </summary>
        /// <value>
        /// The json response.
        /// </value>
        IJsonResponse JsonResponse { set; get; }

    }
}