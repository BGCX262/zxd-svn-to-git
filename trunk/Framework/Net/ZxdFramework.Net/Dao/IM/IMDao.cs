using System;
using System.Collections.Generic;
using NHibernate;
using Spring.Data.NHibernate.Generic;
using Spring.Transaction.Interceptor;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.IM;
using System.Linq;

namespace ZxdFramework.Dao.IM
{

    /// <summary>
    ///  定义即时通信用户数据仓库
    /// </summary>
    public interface IIMDao
    {

        /// <summary>
        /// 获取在线用户对象
        /// </summary>
        /// <value>The on line users.</value>
        IDictionary<Guid, IMUser> OnLineUsers { get; }

        /// <summary>
        /// 获取用户的即时通讯对象. 如果数据库中没有当前对象. 那么将会创建这个对象
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        IMUser Join(Guid id, States state);

        /// <summary>
        /// 退出当前登录的用户
        /// </summary>
        /// <param name="id">The id.</param>
        void OffLine(Guid id);


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void SentMessage(Message msg);

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command">The command.</param>
        void SentCommand(IMCommand command);

    }


    /// <summary>
    /// 
    /// </summary>
    public class IMDao : DaoSupport, IIMDao
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IMDao"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public IMDao(ISessionFactory factory) : base(factory)
        {
            OnLineUsers = new Dictionary<Guid, IMUser>(200);
        }

        public IDictionary<Guid, IMUser> OnLineUsers { protected set; get; }

        [Transaction]
        public IMUser Join(Guid id, States state)
        {
            var m = Session.Get<IMUser>(id);
            if(m == null)
            {
                m = new IMUser();
                var groups = new List<IMUserGroup>
                                 {
                                     new IMUserGroup()
                                         {
                                             Type = IMGroupTypes.自己
                                         }
                                 };
                m.Groups = groups;
                Session.Save(m);
                Session.Flush();
            }

            //m.Groups.Where(g => g.Type == IMGroupTypes.自己).First().Members.First().State = state;

            if(OnLineUsers.ContainsKey(m.Id))
            {
                OnLineUsers[m.Id] = m;
            }
            else
            {
                OnLineUsers.Add(m.Id, m);
            }

            return m;
        }

        public void OffLine(Guid id)
        {
            throw new NotImplementedException();
        }

        public void SentMessage(Message msg)
        {
           //var toList = OnLineUsers.Values.Where(u )
        }

        public void SentCommand(IMCommand command)
        {
            
        }
    }
}