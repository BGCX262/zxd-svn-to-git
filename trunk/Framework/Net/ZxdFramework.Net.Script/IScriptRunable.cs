using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Net.Script
{
    /// <summary>
    /// ���嵱ǰ�������Ƿ�֧�ֽű�����
    /// </summary>
    public interface IScriptRunable
    {
        /// <summary>
        /// ִ�е�ǰ�����ж���Ľű�
        /// </summary>
        /// <param name="engine">�ű���������</param>
        /// <param name="scope">���л���</param>
        /// <returns>
        /// ���ؽ�ִ�еĽ��
        /// </returns>
        object ScriptRun(ScriptEngine engine, ScriptScope scope);

    }
}