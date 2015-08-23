using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Common.Logging;
using Microsoft.Scripting.Hosting;
using ZxdFramework.Authorization.Services;
using ZxdFramework.DataContract;
using ZxdFramework.Events;
using ZxdFramework.Script;

namespace ZxdFramework.Authorization
{
    #region 接口
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorizationFactory
    {
        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        IEventAggregator EventAggregator { get; }

        /// <summary>
        /// 获取权限的服务层
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        IAuthorityService AuthorityService { get; }

        /// <summary>
        /// 权限脚本运行引擎
        /// </summary>
        /// <value>
        /// The script engine.
        /// </value>
        ScriptEngine ScriptEngine { get; }

        /// <summary>
        /// Gets or sets the script scope.
        /// </summary>
        /// <value>
        /// The script scope.
        /// </value>
        ScriptScope ScriptScope { get; }

        /// <summary>
        /// 获取根角色
        /// </summary>
        /// <value>
        /// The root role.
        /// </value>
        IRole RootRole { get; }

        /// <summary>
        /// 获取安全节点
        /// </summary>
        /// <value>
        /// The root action.
        /// </value>
        ISecurityAction RootAction { get; set; }

        /// <summary>
        /// 重新加载角色
        /// </summary>
        void ReloadRole();

        /// <summary>
        /// 重新加载安全配置
        /// </summary>
        void ReloadSecurityAction(ICollection<SecurityActionResource> resources);

        /// <summary>
        /// 在当前安全节点中增加节点配置
        /// </summary>
        /// <param name="resource">The resource.</param>
        void AddSecurityAction(SecurityActionResource resource);

        /// <summary>
        /// 在当前安全节点中增加节点配置
        /// </summary>
        /// <param name="resources">The resources.</param>
        void AddSecurityAction(ICollection<SecurityActionResource> resources);

        /// <summary>
        /// 检查用户的是否具有当前节点的权限
        /// </summary>
        /// <param name="user">检查用户</param>
        /// <param name="action">安全节点</param>
        /// <param name="isRunScript">是否进行脚本检查</param>
        /// <returns></returns>
        bool CheckSecurityAction(IUser user, ISecurityAction action, bool isRunScript);
    }
    #endregion


    /// <summary>
    /// 权限检查工厂
    /// </summary>
    public class AuthorizationFactory : IAuthorizationFactory, IAuthorizationFilter
    {
        private IDictionary<string, IRole> _roles = new Dictionary<string, IRole>();
        private Dictionary<string, ISecurityAction> _securityActions = new Dictionary<string, ISecurityAction>();
        private IRole _rootRole;
        private ILog _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFactory"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="scriptFactory">The script factory.</param>
        public AuthorizationFactory(IAuthorityService service, IEventAggregator eventAggregator, IScriptFactory scriptFactory)
        {
            InitScript(scriptFactory);
            AuthorityService = service;
            ReloadSecurityAction(new List<SecurityActionResource>());
            EventAggregator = eventAggregator;
            EventAggregator.Subscribe<RoleChangedEvent>(a =>
                                                            {
                                                                if (_log.IsDebugEnabled) _log.Debug("监测到角色变化事件" + a.Target);
                                                                ReloadRole();
                                                            });
            ReloadRole();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static AuthorizationFactory Instance
        {
            get { return "AuthorizationFactory".GetInstance<AuthorizationFactory>(); }
        }


        #region 公共属性

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator { protected set; get; }

        /// <summary>
        /// 获取权限的服务层
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        public IAuthorityService AuthorityService { protected set; get; }


        /// <summary>
        /// 权限脚本运行引擎
        /// </summary>
        /// <value>
        /// The script engine.
        /// </value>
        public ScriptEngine ScriptEngine { protected set; get; }


        /// <summary>
        /// Gets or sets the script scope.
        /// </summary>
        /// <value>
        /// The script scope.
        /// </value>
        public ScriptScope ScriptScope { protected set; get; }

        #endregion



        //public ISecurityService

        /// <summary>
        /// 获取根角色
        /// </summary>
        /// <value>
        /// The root role.
        /// </value>
        public IRole RootRole
        {
            get { return _rootRole; }
            private set
            {
                if(value != null)
                {
                    _roles.Clear();
                    TackRole(value);
                }
                _rootRole = value;
            }
        }

        /// <summary>
        /// 重新加载角色
        /// </summary>
        public void ReloadRole()
        {
            if (_log.IsWarnEnabled) _log.Warn("重新加载角色列表. ");
            RootRole = AuthorityService.GetRootRole();
        }


        private ISecurityAction _rootAction;
        /// <summary>
        /// 获取安全节点
        /// </summary>
        /// <value>
        /// The root action.
        /// </value>
        public ISecurityAction RootAction
        {
            get { return _rootAction; }
            set
            {
                TackAction(value);
                _rootAction = value;
            }
        }


        /// <summary>
        /// 重新加载安全配置
        /// </summary>
        public void ReloadSecurityAction(ICollection<SecurityActionResource> resources)
        {
            _securityActions.Clear();
            if (_log.IsDebugEnabled) _log.Debug("重置权限根节点");
            RootAction = AuthorityService.GetRootSecurityAction();

            AddSecurityAction(resources);
        }


        /// <summary>
        /// 在当前安全节点中增加节点配置
        /// </summary>
        /// <param name="resource">The resource.</param>
        public void AddSecurityAction(SecurityActionResource resource)
        {
            if (_log.IsWarnEnabled) _log.Warn("增加权限节点 :" + resource.GetType().FullName);
            foreach (var action in resource.GetResourceActions())
            {
                _securityActions.Add(action.Key, action);
            }
            MakeActions();
        }

        /// <summary>
        /// 在当前安全节点中增加节点配置
        /// </summary>
        /// <param name="resources">The resources.</param>
        public void AddSecurityAction(ICollection<SecurityActionResource> resources)
        {
            
            if (resources == null) throw new ArgumentNullException("resources");
            if (_log.IsWarnEnabled) _log.Warn("增加权限节点 :" + resources);
            foreach (var action in
                resources.Select(resource => resource.GetResourceActions()).TakeWhile(acitons => acitons != null).SelectMany(acitons => acitons))
            {
                _securityActions.Add(action.Key, action);
            }
            MakeActions();
        }


        /// <summary>
        /// 检查用户的是否具有当前节点的权限
        /// </summary>
        /// <param name="user">检查用户</param>
        /// <param name="action">安全节点</param>
        /// <param name="isRunScript">是否进行脚本检查</param>
        /// <returns></returns>
        public bool CheckSecurityAction(IUser user, ISecurityAction action, bool isRunScript)
        {

            if (!isRunScript)
            {
                return user.RoleNames.Any(n => _roles.ContainsKey(n) && _roles[n].Contain(action.Key));
            }


            var scope = ScriptEngine.CreateScope();
            
            scope.SetVariable("User", user);
            scope.SetVariable("Action", action);
            scope.SetVariable("用户", user);
            
            scope.SetVariable("安全节点", action);
            scope.SetVariable("通过", true);
            scope.SetVariable("拒绝", false);


            return user.RoleNames.Any(n => _roles.ContainsKey(n) && _roles[n].Contain(action, ScriptEngine, ScriptScope));
        }


        #region 私有方法

        /// <summary>
        /// 初始化脚本运行环境
        /// </summary>
        /// <param name="factory">The factory.</param>
        private void InitScript(IScriptFactory factory)
        {
            var objs = new List<Type>
                           {
                               typeof(IUser),
                               typeof(IScriptFactory),
                               typeof(IEncrypt),
                               typeof(IScriptFactory),
                               GetType()
                           };

            var runtime = factory.CreateIronRubyRuntime(objs, true, false);
            ScriptEngine = runtime.GetEngine("ruby");
            ScriptScope = ScriptEngine.CreateScope();
        }

        /// <summary>
        /// 递归提取所有的角色
        /// </summary>
        /// <param name="roles">The roles.</param>
        private void TackRole(IRole role)
        {
            if (role == null) return;
            _roles.Add(role.Name, role);

            if (role.Children != null)
                foreach (var r in role.Children)
                {

                    TackRole(r);
                }
        }

        /// <summary>
        /// Tacks the action.
        /// </summary>
        /// <param name="action">The action.</param>
        private void TackAction(ISecurityAction action)
        {
            if (action == null) return;
            _securityActions.Add(action.Key, action);

            if (action.Children == null) return;
            foreach (var child in action.Children)
            {
                TackAction(child);
            }

        }


        /// <summary>
        /// 整理安全节点
        /// </summary>
        private void MakeActions()
        {
            var key = "";
            var index = -1;
            var re = false;

            foreach (var securityAction in _securityActions)
            {
                key = securityAction.Key;
                //if(string.IsNullOrEmpty(key)) continue;
                if (securityAction.Value == RootAction) continue;

                index = -1;
                re = false;
                while ((index = key.LastIndexOf(".")) > 0)
                {
                    key = key.Substring(0, index);
                    if (_securityActions.ContainsKey(key))
                    {
                        var parent = _securityActions[key];
                        parent.AddChild(securityAction.Value);
                        re = true;
                        break;
                    }
                }

                if (!re)
                {
                    RootAction.AddChild(securityAction.Value);
                }
            }
        }
        #endregion

        /// <summary>
        /// 进行 MVC 请求的权限验证
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if(Globals.IsDebug) return;
            var sec = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SecurityAttribute), true);
            if (sec.Length == 0) return;

            if(!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedException();
            }

            var user = AuthorityService.CurrentUser;

            foreach (var o in sec)
            {
                var temp = (SecurityAttribute) o;

                if (string.IsNullOrEmpty(temp.ActionKey)) return;

                var action = _securityActions[temp.ActionKey];
                if (!CheckSecurityAction(user, action, true))
                    throw new SecurityException();
            }
            
        }
    }
}