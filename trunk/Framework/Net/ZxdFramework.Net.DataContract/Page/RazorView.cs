using System;

namespace ZxdFramework.DataContract.Page
{
    /// <summary>
    /// Razor 视图模型
    /// </summary>
    public class RazorView : Entity<Guid>
    {
        /// <summary>
        /// 设置获取视图名称
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { set; get; }

        /// <summary>
        /// 设置获取控制器名称, 唯一的视图名称. 可作为获取模板视图的主键
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public virtual string ControllerName { set; get; }


        /// <summary>
        /// 视图模板的说明
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { set; get; }


        /// <summary>
        /// 设置获取模板更新的时间
        /// </summary>
        /// <value>
        /// The update time.
        /// </value>
        public virtual DateTime UpdateTime { set; get; }


        /// <summary>
        /// 创建或者更新用户
        /// </summary>
        /// <value>
        /// The create user.
        /// </value>
        public virtual Guid UpdateUser { set; get; }

        /// <summary>
        /// 设置获取视图的内容
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual string Content { set; get; }

        /// <summary>
        /// 模板类型
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual ViewCategory Category { set; get; }
    }


    /// <summary>
    /// 视图类型
    /// </summary>
    public enum ViewCategory
    {
        /// <summary>
        /// 模板
        /// </summary>
        Master,
        /// <summary>
        /// 视图模板
        /// </summary>
        View,
        /// <summary>
        /// 部分
        /// </summary>
        Partial,
        /// <summary>
        /// 共享
        /// </summary>
        Shared
    }
}