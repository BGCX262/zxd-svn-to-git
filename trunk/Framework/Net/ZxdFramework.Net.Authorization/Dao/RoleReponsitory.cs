using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using Spring.Data.NHibernate.Generic;
using Spring.Transaction;
using Spring.Transaction.Interceptor;
using ZxdFramework.Dao;
using ZxdFramework.DataContract;
using ZxdFramework.Events;

namespace ZxdFramework.Authorization.Dao
{

    /// <summary>
    /// 操作用户角色
    /// </summary>
    public interface IRoleReponsitory : IRepositoryBase<Role, Guid>
    {
        /// <summary>
        /// 检查是否具有当前用户角色
        /// </summary>
        /// <param name="rolename">The rolename.</param>
        /// <returns></returns>
        bool Contains(string rolename);

        /// <summary>
        /// Gets or sets the <see cref="ZxdFramework.Authorization.Role"/> with the specified ERROR.
        /// </summary>
        Role this[string name] { get; }

        /// <summary>
        /// 获取根角色下的子角色
        /// </summary>
        /// <returns></returns>
        IList<IRole> GetRootChildren();

        /// <summary>
        /// Gets the root.
        /// </summary>
        /// <returns></returns>
        IRole GetRoot();

    }

    internal class RoleReponsitory : RepositoryBase<Role, Guid>, IRoleReponsitory
    {
        private ILog _log = LogManager.GetCurrentClassLogger();

        public RoleReponsitory(HibernateTemplate template, IEventAggregator eventAggregator) : base(template)
        {
            EventAggregator = eventAggregator;
        }

        public IEventAggregator EventAggregator { protected set; get; }

        [Transaction]
        [EventPublish(typeof(RoleChangedEvent))]
        public override void Add(Role item)
        {
           if(_log.IsDebugEnabled) _log.Debug("增加一个角色 :" + item.Name);
           Session.Save(item);
        }

        public override void Add(IModel item)
        {
            Add((Role)item);
        }

        [Transaction]
        [EventPublish(typeof(RoleChangedEvent))]
        public override void Remove(Role item)
        {
            if (_log.IsDebugEnabled) _log.Debug("删除一个角色 :" + item.Name);
            base.Remove(item);
            EventAggregator.Publish(new RoleChangedEvent(item));
        }

        [Transaction(ReadOnly = true)]
        public Role Get(string rolename)
        {
            return (from role in Session.Query<Role>()
                        where role.Name.ToLower() == rolename.ToLower()
                        select role).FirstOrDefault();

        }

        
        public bool Contains(string rolename)
        {
            return Get(rolename) != null;
        }

        
        public Role this[string name]
        {
            get { return Get(name); }
        }

        [Transaction(ReadOnly = true)]
        public IList<IRole> GetRootChildren()
        {
            //SessionFactory.OpenStatelessSession().Query
           var data = (from role in Session.Query<Role>() where role.Parent == null select (IRole)role).ToList();
           // Session.Clear();
            return data;
        }

        
        public IRole GetRoot()
        {
            return new Role
            {
                Name = "根角色",
                Children = GetRootChildren()
            };
        }

    }
}