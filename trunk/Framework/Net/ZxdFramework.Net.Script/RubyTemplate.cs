using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using ZxdFramework.DataContract;

namespace ZxdFramework.Script
{
    /// <summary>
    /// Ruby �ű�ģ��, ����ʹ�����ģ�����ɱ�׼�Ľű����
    /// </summary>
    public class RubyTemplate : Entity<Guid>
    {
        private ILog _log = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Initializes a new instance of the <see cref="RubyTemplate"/> class.
        /// </summary>
        public RubyTemplate()
        {
            ModuleName = "";
            Requires = new List<string>();
            Footnode = new List<string>();
            Roles = new HashSet<string>();
            Users = new HashSet<string>();
            //IsAnalyze = true;
        }


        /// <summary>
        /// ���û�ȡ��ǰ�ű�������ģ���еĽṹ. ģ���� . ��Ϊ�ָ���
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { set; get; }

        /// <summary>
        /// �ű�����ʽ����
        /// </summary>
        /// <value>
        /// The name of the script.
        /// </value>
        public string ScriptName { set; get; }


        /// <summary>
        /// ���û�ȡ�ű��ı���
        /// </summary>
        /// <value>
        /// The name of the alia.
        /// </value>
        public string AliasName { set; get; }


        /// <summary>
        /// ��ȡ�ű�ͷ���ıر���Ϣ. ���������������������Ŀ�Բ�������ɺ�Ľű�
        /// </summary>
        /// <value>
        /// The requires.
        /// </value>
        public ICollection<string> Requires { protected set; get; }


        /// <summary>
        /// ��ȡ����Ľű�֮�����Ϣ. 
        /// </summary>
        /// <value>
        /// The footnodes.
        /// </value>
        public ICollection<string> Footnode { protected set; get; }

        /// <summary>
        /// ���û�ȡ�ű�������
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { set; get; }


        /// <summary>
        /// ���û�ȡ�Ƿ�ʹ�õ�ǰ�����Խ����ű�, ���Ϊ False ��ôֱ�ӷ��� Content �ű�����
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is analyze; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnalyze { set; get; }


        /// <summary>
        /// �ű�����ʱ��
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public DateTime LastUpdate { set; get; }


        private string _userId;
        /// <summary>
        /// ���������û�, �����û�Ĭ�ϻ���뵽�ű��������û�
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public string UserId
        {
            get { return _userId; }
            set
            {
                //if (value != _userId && !string.IsNullOrEmpty(value))
                //{
                //    if (Users == null) Users = new HashSet<string>();
                //    if (!Users.Contains(value)) Users.Add(value);
                //}
                _userId = value;
            }
        }


        /// <summary>
        /// ���û�ȡ��ǰ�Ľű������Ľ�ɫ
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public ICollection<string> Roles { set; get; }


        /// <summary>
        /// ���û�ȡ��ǰ�Ľű��������û�
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ICollection<string> Users { set; get; }


        /// <summary>
        /// ��ȡ�ű�
        /// </summary>
        /// <returns></returns>
        public string ToScript()
        {
            if (!IsAnalyze) return Content;

            if (string.IsNullOrWhiteSpace(ScriptName)) throw new ZxdFrameworkException("���庯�������Ʋ���Ϊ��");

            var script = new StringBuilder();
            //����ر�
            foreach (var require in Requires)
            {
                script.AppendLine(require);
            }

            var modules = ModuleName.Split('.');

            //����ģ��ṹ
            foreach (var module in modules)
            {
                if (!string.IsNullOrWhiteSpace(module)) script.AppendLine("module " + module);
            }

            //�����ű�
            script.AppendLine();
            script.AppendLine("def " + ScriptName);
            script.AppendLine(Content);
            script.AppendLine("end");
            script.AppendLine();
            if (!string.IsNullOrWhiteSpace(AliasName))
            {
                script.AppendLine("alias " + AliasName + " " + ScriptName);
            }

            foreach (var foot in Footnode)
            {
                script.AppendLine(foot);
            }


            foreach (var module in modules)
            {
                if (!string.IsNullOrWhiteSpace(module)) script.AppendLine("end");
            }

            var data = script.ToString();

            if (_log.IsDebugEnabled) _log.Debug("�ű����: \n" + data);

            return data;
        }

    }
}