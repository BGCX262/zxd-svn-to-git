using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Spring.Data.NHibernate.Generic;
using Spring.Transaction.Interceptor;
using ZxdFramework.Dao;
using ZxdFramework.DataContract;
using ZxdFramework.Model;

namespace ZxdFramework.Authorization.Dao
{
    /// <summary>
    /// �����˺Ų����ӿ�
    /// </summary>
    public interface IAccountRepository : IRepositoryBase<User, Guid>
    {
        /// <summary>
        /// ��֤�û�, �ҵ�ǰ�û�״̬�����Ǽ����
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool ValidateUser(string username, string password);


        /// <summary>
        /// ʹ���û�����ȡ�û�
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        User GetUser(string username);


        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user">The user.</param>
        void CreateUser(User user);

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="username">The username.</param>
        void Remove(string username);


        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user">The user.</param>
        void UpdateUser(User user);


        /// <summary>
        /// ������߸����û���Ƭ
        /// </summary>
        /// <param name="userPhoto">The user photo.</param>
        void UpdateOrSavePhoto(BaseUserPhoto userPhoto);


        /// <summary>
        /// ��ȡ�û�����Ƭ
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        BaseUserPhoto GetUserPhoto(Guid id);


        /// <summary>
        /// ��ȡ�û���Ƭ
        /// </summary>
        /// <param name="username">�û���¼����</param>
        /// <returns></returns>
        BaseUserPhoto GetUserPhoto(string username);


        /// <summary>
        /// ������߸����û�������
        /// </summary>
        /// <param name="setting">The setting.</param>
        void SaveOrUpdateSetting(UserSetting setting);


        /// <summary>
        /// ��ȡ�û�������ֵ, ����������õ�ֵ���û��. ����ϲ��ο��û�������ֵ
        /// </summary>
        /// <param name="userid">The userid.</param>
        IList<UserSetting> GetUserSettings(Guid userid);
    }


    /// <summary>
    /// �˺Ų�����
    /// </summary>
    internal class AccountRepository : RepositoryBase<User, Guid>, IAccountRepository
    {
        protected AccountRepository(HibernateTemplate template, IEncrypt encrypt)
            : base(template)
        {
            Encrypt = encrypt;
        }

        public IEncrypt Encrypt { protected set; get; }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);
            return user != null
                   && user.Password == Encrypt.Encrypt(password)
                   && user.State == UserStates.Active;
        }

        public User GetUser(string username)
        {

            var data = (from user in Session.Query<User>()
             where user.LoginName.ToLower() == username.ToLower()
             select user).FirstOrDefault();

            return data;
        }

        [Transaction]
        public void CreateUser(User user)
        {
            user.Password = Encrypt.Encrypt(user.Password);
            //Save(user);
            Add(user);
        }

        public void Remove(string username)
        {
            var user = GetUser(username);
            Remove(user);
        }

        [Transaction]
        public void UpdateUser(User user)
        {
            Session.Update(user);
        }

        [Transaction]
        public void UpdateOrSavePhoto(BaseUserPhoto userPhoto)
        {
            Session.SaveOrUpdate(userPhoto);
        }

        public BaseUserPhoto GetUserPhoto(Guid id)
        {
            return (from p in Session.Query<UserPhoto>() where p.Id == id select p).FirstOrDefault();
        }

        public BaseUserPhoto GetUserPhoto(string username)
        {
            return
                (from p in Session.Query<UserPhoto>() where p.Owner.LoginName.ToLower() == username.ToLower() select p).
                    FirstOrDefault();
        }

        [Transaction]
        public void SaveOrUpdateSetting(UserSetting setting)
        {
            Session.SaveOrUpdate(setting);
        }

        public IList<UserSetting> GetUserSettings(Guid userid)
        {
            var gdata = (from p in Session.Query<UserSetting>()
                         from u in Session.Query<User>()
                         where p.OwnerId == u.Id && u.LoginName == "Guest"
                         select p).ToList();

            var data = (from p in Session.Query<UserSetting>() where p.OwnerId == userid select p).ToList();

            if (data.Count == 0) return gdata;

            foreach (var setting in
                from setting in gdata
                where !data.Any(userSetting => userSetting.Key == setting.Key)
                select setting)
            {
                data.Add(setting);
            }

            return data;
        }
    }
}