using System.Collections.Generic;
using System.Linq;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 安全节点的描述资源类, 在当前类中定义的字段常量都会作为安全树中的节点. <br />
    /// 并且在这个节点中以(_)下滑线作为父子集的关系, 常量的值就会被作为权限节点的唯一键. 且这个键是不区分大小写的.
    /// <br />
    /// 
    /// 列: public count string ModuleName_ActionName_ChildName = "ModuleName.ActionName.ChildName";
    /// 
    /// </summary>
    public abstract class SecurityActionResource
    {
        /// <summary>
        /// 获取当前资源中所定义的权限列表. <br />
        /// <b>请注意, 发回的节点列表并没有父子集的关系</b>
        /// </summary>
        /// <returns></returns>
        public ISet<ISecurityAction> GetResourceActions()
        {

            var type = GetType();
            var fields = type.GetFields();

            var actions = new HashSet<ISecurityAction>();

            //提取资源中的配置
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