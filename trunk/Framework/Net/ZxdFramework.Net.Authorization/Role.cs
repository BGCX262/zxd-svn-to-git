using System;
using Microsoft.Scripting.Hosting;
using ZxdFramework.DataContract;
using ZxdFramework.Net.Script;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// �û���ɫ, ��֧��Ȩ�޼��ǰ�Ľű�����
    /// </summary>
    public class Role : BaseRole, IScriptRunable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            IsComplied = false;
        }

        private string _authorityScript;
        /// <summary>
        /// ���û�ȡȨ�޼��ű�
        /// </summary>
        /// <value>
        /// The security script.
        /// </value>
        public string AuthorityScript
        {
            get { return _authorityScript; }
            set
            {
                if (value != AuthorityScript) IsComplied = false;
                _authorityScript = value;
            }
        }



        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public DateTime LastUpdate { set; get; }


        protected bool IsComplied { set; get; }
        protected CompiledCode CompiledCode { set; get; }
        /// <summary>
        /// ִ��Ȩ�޼��ǰ�Ľű�
        /// </summary>
        /// <param name="engine">�ű���������</param>
        /// <param name="scope">���л���</param>
        /// <returns>
        /// ���ؽ�ִ�еĽ��
        /// </returns>
        public object ScriptRun(ScriptEngine engine, ScriptScope scope)
        {
            if (string.IsNullOrWhiteSpace(AuthorityScript)) return false;

            if (!IsComplied)
            {
                CompiledCode = engine.CreateScriptSourceFromString(AuthorityScript).Compile();
                IsComplied = true;
            }
            
            
            var result = CompiledCode.Execute(scope);
            return result is bool ? (bool) result : false;
        }

        public override bool Contain(ISecurityAction action, ScriptEngine engine,  ScriptScope scope)
        {
            return Contain(action.Key, engine, scope);
        }

        public override bool Contain(string key, ScriptEngine engine, ScriptScope scope)
        {
            var result = base.Contain(key);
           return (bool)ScriptRun(engine, scope) ? true : result;
        }


        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode()*12;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as IRole;
            if (temp == null) return false;
            return string.Equals(temp.Name, Name);

        }
    }
}