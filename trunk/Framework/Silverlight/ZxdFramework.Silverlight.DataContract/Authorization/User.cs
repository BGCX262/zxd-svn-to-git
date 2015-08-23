using System;

namespace ZxdFramework.DataContract.Authorization
{
    /// <summary>
    /// ֧�ְ���ͼ���û�����ģ��
    /// </summary>
    public class User : ViewModel, IUser
    {
        private Category _category;
        private string _loginName;
        private string _name;
        private string _password;
        private string[] _roleNames;
        private UserStates _state;

        #region IUser Members

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public Guid Id { set; get; }

        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        /// <value></value>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Notify(() => Name);
                }
            }
        }


        /// <summary>
        /// ��ȡ�û�������
        /// </summary>
        /// <value>The name of the login.</value>
        public string LoginName
        {
            get { return _loginName; }
            set
            {
                if (_loginName != value)
                {
                    _loginName = value;
                    Notify(() => LoginName);
                }
            }
        }

        /// <summary>
        /// ��¼����
        /// </summary>
        /// <value></value>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    Notify(() => Password);
                }
            }
        }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        /// <value></value>
        public DateTime? LoginTime { set; get; }

        /// <summary>
        /// ���û�ȡ�û���״̬<br/>
        /// ��ǰ��״ֵ̬������ϵͳ�ж��û��Ŀ���.
        /// </summary>
        /// <value>The state.</value>
        public UserStates State
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    Notify(() => State);
                }
            }
        }

        /// <summary>
        /// ��ǰ�û��Ľ�ɫ����
        /// </summary>
        /// <value></value>
        public string[] RoleNames
        {
            get { return _roleNames; }
            set
            {
                if (_roleNames != value)
                {
                    _roleNames = value;
                    Notify(() => RoleNames);
                }
            }
        }


        /// <summary>
        /// ���û�ȡ�û��ı�� ID, �������û��Ƿ��ظ���¼�ı��. ÿ��
        /// </summary>
        /// <value>The token id.</value>
        public string TokenId { set; get; }

        /// <summary>
        /// ���û�ȡ��¼�Ĵ���
        /// </summary>
        /// <value>The count.</value>
        public int Count { set; get; }

        /// <summary>
        /// ���û�ȡ���һ�ε�¼��IP
        /// </summary>
        /// <value>The last ip.</value>
        public string LastIp { set; get; }

        /// <summary>
        /// ���û�ȡ�û�����
        /// </summary>
        /// <value>The category.</value>
        public Category Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    Notify(() => Category);
                }
            }
        }

        #endregion
    }
}