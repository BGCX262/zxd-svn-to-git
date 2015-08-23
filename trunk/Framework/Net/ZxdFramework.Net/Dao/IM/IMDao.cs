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
    ///  ���弴ʱͨ���û����ݲֿ�
    /// </summary>
    public interface IIMDao
    {

        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <value>The on line users.</value>
        IDictionary<Guid, IMUser> OnLineUsers { get; }

        /// <summary>
        /// ��ȡ�û��ļ�ʱͨѶ����. ������ݿ���û�е�ǰ����. ��ô���ᴴ���������
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        IMUser Join(Guid id, States state);

        /// <summary>
        /// �˳���ǰ��¼���û�
        /// </summary>
        /// <param name="id">The id.</param>
        void OffLine(Guid id);


        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void SentMessage(Message msg);

        /// <summary>
        /// ��������
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
                                             Type = IMGroupTypes.�Լ�
                                         }
                                 };
                m.Groups = groups;
                Session.Save(m);
                Session.Flush();
            }

            //m.Groups.Where(g => g.Type == IMGroupTypes.�Լ�).First().Members.First().State = state;

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