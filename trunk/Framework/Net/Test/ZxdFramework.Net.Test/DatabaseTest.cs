using System;
using System.Collections.Generic;
using System.IO;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using ZxdFramework.Authorization;
using ZxdFramework.Dao.System;
using ZxdFramework.DataContract;
using ZxdFramework.DataContract.IM;
using ZxdFramework.DataContract.System;
using ZxdFramework.Mvc.Services;
using ZxdFramework.Script;
using ZxdFramework.Net.Script;
using ZxdFramework.Mvc;

namespace ZxdFramework
{
    [TestFixture]
    public class DatabaseTest
    {



        /// <summary>
        /// ��ʼ�����ݿ�
        /// </summary>
        [Test]
        public void TestCreate()
        {
            //NHibernate.Impl.SessionFactoryImpl
            var config = new Configuration();
            config.Configure("DatabaseCreate.cfg.xml");

            config.AddAssembly(typeof(IModel).Assembly);
            config.AddAssembly(typeof(User).Assembly);


            config.AddAssembly(typeof(Bootstrap).Assembly);
            config.AddAssembly(typeof(DESEncrypt).Assembly);
            config.AddAssembly(typeof(JsonResult).Assembly);
            //config.AddClass(typeof (UserPhoto));
            //config.AddClass(typeof (User));
            //config.AddAssembly(typeof (IModel).Assembly);
            //config.AddAssembly(typeof (User).Assembly);


            #region �û����ݱ�
            
            config.AddClass(typeof(UserPhoto));
            config.AddClass(typeof(User));
            config.AddClass(typeof(UserSetting));

            #endregion

            //Log.Debug("��ʼ�����");
            //config.AddAssembly(typeof())
            var schem = new SchemaExport(config);
            
            //schem.Drop(true, true);
            schem.Create(true, true);
        }

        [Test]
        public void TestCreateIM()
        {

            //NHibernate.Impl.SessionFactoryImpl
            var config = new Configuration();
            config.Configure("DatabaseCreate.cfg.xml");

            config.AddClass(typeof (IMUser));
            config.AddClass(typeof (IMUserGroup));
            config.AddClass(typeof(IMUserSetting));

            //Log.Debug("��ʼ�����");
            //config.AddAssembly(typeof())
            var schem = new SchemaExport(config);

            //schem.Drop(true, true);
            schem.Create(true, true);
        }
    }


    public class DatabaseInit : TestSupport
    {
        [Test]
        public void CreateGuestRole()
        {
            var role = new Role()
                           {
                               Name = "������ɫ"
                           };

            ServiceFactory.AuthorityService.RoleReponsitory.Add(role);
            //CreateBaseAccount();
        }


        /// <summary>
        /// �����������û�����
        /// </summary>
        [Test]
        public void CreateBaseAccount()
        {
            var user = new User
            {
                Name = "�ο�",
                LoginName = "Guest",
                RoleNames = new string[] { "������ɫ" },
                LoginTime = DateTime.Now,
                Category = Category.System
            };

            ServiceFactory.AuthorityService.AccountRepository.CreateUser(user);

            var admin = new User
                            {
                                Name = "��������Ա",
                                LoginName = "Administrator",
                                Password = "123456"
                            };

            ServiceFactory.AuthorityService.AccountRepository.CreateUser(admin);

            CreateUserPhoto();
        }

        [Test]
        public void CreateUserPhoto()
        {
            var path = @"E:\Projects\SilverlightProject_2.0\Framework\Net\ZxdFramework.WebUI\Styles\lighttheme\Images\user.png";

            var f = new FileInfo(path);




            var photo = new UserPhoto
                            {
                                Owner = ServiceFactory.AuthorityService.AccountRepository.GetUser("Guest"),
                                Type = f.FullName.Substring(f.FullName.LastIndexOf(".")+1).ToLower(),
                                Data = (new System.IO.BinaryReader(f.Open(FileMode.Open))).ReadBytes((int)f.Length)
                            };

            ServiceFactory.AuthorityService.AccountRepository.UpdateOrSavePhoto(photo);

            path = @"E:\Projects\SilverlightProject_2.0\Framework\Net\ZxdFramework.WebUI\Styles\lighttheme\Images\admin.png";

            f = new FileInfo(path);

            photo = new UserPhoto
            {
                Owner = ServiceFactory.AuthorityService.AccountRepository.GetUser("Administrator"),
                Type = f.FullName.Substring(f.FullName.LastIndexOf(".")+1).ToLower(),
                Data = (new System.IO.BinaryReader(f.Open(FileMode.Open))).ReadBytes((int)f.Length)
            };

            ServiceFactory.AuthorityService.AccountRepository.UpdateOrSavePhoto(photo);
        }


        /// <summary>
        /// ��ʼ��ģ������
        /// </summary>
        [Test]
        public void InitModules()
        {
            var moduleFile = new ModuleFile
                                 {
                                     FileName = "ZxdFramework.Silverlight.Shell.UI.xap",
                                     UploadUser = "System",
                                     ModuleList = new List<ModuleInfo>
                                                      {
                                                          new ModuleInfo
                                                              {
                                                                  Author = "����",
                                                                  Category = "System",
                                                                  Description = "���������ʾģ��",
                                                                  ModuleType = "ZxdFramework.Silverlight.Shell.UI.ShellModule, ZxdFramework.Silverlight.Shell.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                                                                  ModuleName = "shell",
                                                                  Ref = "ZxdFramework.Silverlight.Shell.UI.xap",
                                                                  InitializationMode = InitializationMode.WhenAvailable
                                                              }
                                                      }
                                 };

            "ModuleFileRepository".GetInstance<IModuleFileRepository>().Add(moduleFile);
            AddMembershipModule();
            AddSystemManagerModule();
        }

        [Test]
        public void AddMembershipModule()
        {
            var moduleFile = new ModuleFile
            {
                FileName = "ZxdFramework.Silverlight.Membership.UI.xap",
                UploadUser = "System",
                ModuleList = new List<ModuleInfo>
                                                      {
                                                          new ModuleInfo
                                                              {
                                                                  Author = "����",
                                                                  Category = "System",
                                                                  Description = "Ȩ�޽���ģ��. �ṩ���û���¼, Ȩ�����õȽ��洦��",
                                                                  ModuleType = "ZxdFramework.Membership.UI.MembershipModule, ZxdFramework.Silverlight.Membership.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                                                                  ModuleName = "MembershipModule",
                                                                  Ref = "ZxdFramework.Silverlight.Membership.UI.xap",
                                                                  InitializationMode = InitializationMode.WhenAvailable,
                                                                  DependsOn = new []{ "shell" }
                                                              }
                                                      }
            };

            "ModuleFileRepository".GetInstance<IModuleFileRepository>().Add(moduleFile);
        }

        [Test]
        public void AddSystemManagerModule()
        {
            var moduleFile = new ModuleFile
            {
                FileName = "ZxdFramework.Silverlight.SystemManager.xap",
                UploadUser = "System",
                ModuleList = new List<ModuleInfo>
                                                      {
                                                          new ModuleInfo
                                                              {
                                                                  Author = "����",
                                                                  Category = "System.Setting",
                                                                  Description = "��Ҫ����ϵͳ���õ�ģ��",
                                                                  ModuleType = "ZxdFramework.SystemManager.SystemManagerModule, ZxdFramework.Silverlight.SystemManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                                                                  ModuleName = "SystemManager",
                                                                  Ref = "ZxdFramework.Silverlight.SystemManager.xap",
                                                                  InitializationMode = InitializationMode.WhenAvailable,
                                                                  DependsOn = new []{ "shell" }
                                                              }
                                                      }
            };

            "ModuleFileRepository".GetInstance<IModuleFileRepository>().Add(moduleFile);
        }
    }
}