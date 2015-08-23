using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using ZxdFramework.DataContract;

namespace ZxdFramework.Script
{
    /// <summary>
    /// Ruby 脚本模板, 可以使用这个模类生成标准的脚本语句
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
        /// 设置获取当前脚本定义在模块中的结构. 模块以 . 作为分割层次
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { set; get; }

        /// <summary>
        /// 脚本的正式名称
        /// </summary>
        /// <value>
        /// The name of the script.
        /// </value>
        public string ScriptName { set; get; }


        /// <summary>
        /// 设置获取脚本的别名
        /// </summary>
        /// <value>
        /// The name of the alia.
        /// </value>
        public string AliasName { set; get; }


        /// <summary>
        /// 获取脚本头部的必备信息. 可以在这个属性中增加条目以插入的生成后的脚本
        /// </summary>
        /// <value>
        /// The requires.
        /// </value>
        public ICollection<string> Requires { protected set; get; }


        /// <summary>
        /// 获取插入的脚本之后的信息. 
        /// </summary>
        /// <value>
        /// The footnodes.
        /// </value>
        public ICollection<string> Footnode { protected set; get; }

        /// <summary>
        /// 设置获取脚本的内容
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { set; get; }


        /// <summary>
        /// 设置获取是否使用当前的属性解析脚本, 如果为 False 那么直接返回 Content 脚本内容
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is analyze; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnalyze { set; get; }


        /// <summary>
        /// 脚本更新时间
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public DateTime LastUpdate { set; get; }


        private string _userId;
        /// <summary>
        /// 创建更新用户, 更新用户默认会加入到脚本的所属用户
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
        /// 设置获取当前的脚本所属的角色
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public ICollection<string> Roles { set; get; }


        /// <summary>
        /// 设置获取当前的脚本所属的用户
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ICollection<string> Users { set; get; }


        /// <summary>
        /// 获取脚本
        /// </summary>
        /// <returns></returns>
        public string ToScript()
        {
            if (!IsAnalyze) return Content;

            if (string.IsNullOrWhiteSpace(ScriptName)) throw new ZxdFrameworkException("定义函数的名称不能为空");

            var script = new StringBuilder();
            //输出必备
            foreach (var require in Requires)
            {
                script.AppendLine(require);
            }

            var modules = ModuleName.Split('.');

            //定义模块结构
            foreach (var module in modules)
            {
                if (!string.IsNullOrWhiteSpace(module)) script.AppendLine("module " + module);
            }

            //解析脚本
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

            if (_log.IsDebugEnabled) _log.Debug("脚本输出: \n" + data);

            return data;
        }

    }
}