using System.Collections.Generic;
using System.Linq;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// ��ȫ�ڵ��������Դ��, �ڵ�ǰ���ж�����ֶγ���������Ϊ��ȫ���еĽڵ�. <br />
    /// ����������ڵ�����(_)�»�����Ϊ���Ӽ��Ĺ�ϵ, ������ֵ�ͻᱻ��ΪȨ�޽ڵ��Ψһ��. ��������ǲ����ִ�Сд��.
    /// <br />
    /// 
    /// ��: public count string ModuleName_ActionName_ChildName = "ModuleName.ActionName.ChildName";
    /// 
    /// </summary>
    public abstract class SecurityActionResource
    {
        /// <summary>
        /// ��ȡ��ǰ��Դ���������Ȩ���б�. <br />
        /// <b>��ע��, ���صĽڵ��б�û�и��Ӽ��Ĺ�ϵ</b>
        /// </summary>
        /// <returns></returns>
        public ISet<ISecurityAction> GetResourceActions()
        {

            var type = GetType();
            var fields = type.GetFields();

            var actions = new HashSet<ISecurityAction>();

            //��ȡ��Դ�е�����
            foreach (var item in from info in fields
                                 let atrs = info.GetCustomAttributes(typeof(SecurityActionAttribute), true)
                                 where atrs.Length > 0
                                 let at = (SecurityActionAttribute)atrs[0]
                                 select new SecurityAction(((string)info.GetValue(this)).Replace('_', '.').ToLower())
                                 {
                                     Description = at.Description,
                                     Icon = at.Icon,
                                     Name = at.Name
                                 })
            {
                actions.Add(item);
            }

            return actions;
        }
    }
}