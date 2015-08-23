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
    #region �ӿ�
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
        /// ��ȡȨ�޵ķ����
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        IAuthorityService AuthorityService { get; }

        /// <summary>
        /// Ȩ�޽ű���������
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
        /// ��ȡ����ɫ
        /// </summary>
        /// <value>
        /// The root role.
        /// </value>
        IRole RootRole { get; }

        /// <summary>
        /// ��ȡ��ȫ�ڵ�
        /// </summary>
        /// <value>
        /// The root action.
        /// </value>
        ISecurityAction RootAction { get; set; }

        /// <summary>
        /// ���¼��ؽ�ɫ
        /// </summary>
        void ReloadRole();

        /// <summary>
        /// ���¼��ذ�ȫ����
        /// </summary>
        void ReloadSecurityAction(ICollection<SecurityActionResource> resources);

        /// <summary>
        /// �ڵ�ǰ��ȫ�ڵ������ӽڵ�����
        /// </summary>
        /// <param name="resource">The resource.</param>
        void AddSecurityAction(SecurityActionResource resource);

        /// <summary>
        /// �ڵ�ǰ��ȫ�ڵ������ӽڵ�����
        /// </summary>
        /// <param name="resources">The resources.</param>
        void AddSecurityAction(ICollection<SecurityActionResource> resources);

        /// <summary>
        /// ����û����Ƿ���е�ǰ�ڵ��Ȩ��
        /// </summary>
        /// <param name="user">����û�</param>
        /// <param name="action">��ȫ�ڵ�</param>
        /// <param name="isRunScript">�Ƿ���нű����</param>
        /// <returns></returns>
        bool CheckSecurityAction(IUser user, ISecurityAction action, bool isRunScript);
    }
    #endregion


    /// <summary>
    /// Ȩ�޼�鹤��
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
                                                                if (_log.IsDebugEnabled) _log.Debug("��⵽��ɫ�仯�¼�" + a.Target);
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


        #region ��������

        /// <summary>
        /// Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        public IEventAggregator EventAggregator { protected set; get; }

        /// <summary>
        /// ��ȡȨ�޵ķ����
        /// </summary>
        /// <value>
        /// The authority service.
        /// </value>
        public IAuthorityService AuthorityService { protected set; get; }


        /// <summary>
        /// Ȩ�޽ű���������
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
        /// ��ȡ����ɫ
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
        /// ���¼��ؽ�ɫ
        /// </summary>
        public void ReloadRole()
        {
            if (_log.IsWarnEnabled) _log.Warn("���¼��ؽ�ɫ�б�. ");
            RootRole = AuthorityService.GetRootRole();
        }


        private ISecurityAction _rootAction;
        /// <summary>
        /// ��ȡ��ȫ�ڵ�
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
        /// ���¼��ذ�ȫ����
        /// </summary>
        public void ReloadSecurityAction(ICollection<SecurityActionResource> resources)
        {
            _securityActions.Clear();
            if (_log.IsDebugEnabled) _log.Debug("����Ȩ�޸��ڵ�");
            RootAction = AuthorityService.GetRootSecurityAction();

            AddSecurityAction(resources);
        }


        /// <summary>
        /// �ڵ�ǰ��ȫ�ڵ������ӽڵ�����
        /// </summary>
        /// <param name="resource">The resource.</param>
        public void AddSecurityAction(SecurityActionResource resource)
        {
            if (_log.IsWarnEnabled) _log.Warn("����Ȩ�޽ڵ� :" + resource.GetType().FullName);
            foreach (var action in resource.GetResourceActions())
            {
                _securityActions.Add(action.Key, action);
            }
            MakeActions();
        }

        /// <summary>
        /// �ڵ�ǰ��ȫ�ڵ������ӽڵ�����
        /// </summary>
        /// <param name="resources">The resources.</param>
        public void AddSecurityAction(ICollection<SecurityActionResource> resources)
        {
            
            if (resources == null) throw new ArgumentNullException("resources");
            if (_log.IsWarnEnabled) _log.Warn("����Ȩ�޽ڵ� :" + resources);
            foreach (var action in
                resources.Select(resource => resource.GetResourceActions()).TakeWhile(acitons => acitons != null).SelectMany(acitons => acitons))
            {
                _securityActions.Add(action.Key, action);
            }
            MakeActions();
        }


        /// <summary>
        /// ����û����Ƿ���е�ǰ�ڵ��Ȩ��
        /// </summary>
        /// <param name="user">����û�</param>
        /// <param name="action">��ȫ�ڵ�</param>
        /// <param name="isRunScript">�Ƿ���нű����</param>
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
            scope.SetVariable("�û�", user);
            
            scope.SetVariable("��ȫ�ڵ�", action);
            scope.SetVariable("ͨ��", true);
            scope.SetVariable("�ܾ�", false);


            return user.RoleNames.Any(n => _roles.ContainsKey(n) && _roles[n].Contain(action, ScriptEngine, ScriptScope));
        }


        #region ˽�з���

        /// <summary>
        /// ��ʼ���ű����л���
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
        /// �ݹ���ȡ���еĽ�ɫ
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
        /// ����ȫ�ڵ�
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
        /// ���� MVC �����Ȩ����֤
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