using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;



#if SILVERLIGHT
using System.Windows;
using System.Windows.Resources;
#endif

namespace ZxdFramework.DataContract.System
{
    /// <summary>
    /// 模块文件
    /// </summary>
    public class ModuleFile : Entity<Guid>
    {

        private string _fileName;
        public ModuleFile()
        {
            
        }
#if SILVERLIGHT
        public ModuleFile(FileInfo file)
        {
            PackageFile = file;
            ReadModuleFile(PackageFile);
        }
#endif

        /// <summary>
        /// 模块文件
        /// </summary>
        /// <value>The package file.</value>
        [JsonIgnore]
        public FileInfo PackageFile { get; protected set; }


        private ICollection<ModuleInfo> _moduleList;
        /// <summary>
        /// 模块文件中的模块信息列表
        /// </summary>
        /// <value>The module list.</value>
        public ICollection<ModuleInfo> ModuleList
        {
            get { return _moduleList; }
            set
            {
                if(value != null)
                    foreach (var info in value)
                    {
                        info.ModuleFile = this;
                    }
                _moduleList = value;
            }
        }

        /// <summary>
        /// 上传的时间
        /// </summary>
        /// <value>The upload time.</value>
        public DateTime UploadTime { set; get; }

        /// <summary>
        /// 上传用户
        /// </summary>
        /// <value>The upload user.</value>
        public string UploadUser { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return _fileName ?? PackageFile.Name; }
            set { _fileName = value; }
        }

        [JsonProperty]
        public string FileData { protected set; get; }


        public override int GetHashCode()
        {
            return base.GetHashCode() + 235;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ModuleFile)) return false;
            return base.Equals(obj);
        }

#if SILVERLIGHT
        #region 方法

        /// <summary>
        /// 获取程序包中的组件
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        protected IList<AssemblyPart> GetParts(FileInfo file)
        {

            var assemblyParts = new List<AssemblyPart>();


            using (var stream = file.OpenRead())
            {
                var resource = new StreamReader(
                    Application.GetResourceStream(new StreamResourceInfo(stream, null),
                    new Uri("AppManifest.xaml", UriKind.Relative)).Stream
                    );

                using (var xmlReader = XmlReader.Create(resource))
                {
                    xmlReader.MoveToContent();
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Deployment.Parts")
                        {
                            using (XmlReader xmlReaderAssemblyParts = xmlReader.ReadSubtree())
                            {
                                while (xmlReaderAssemblyParts.Read())
                                {
                                    if (xmlReaderAssemblyParts.NodeType == XmlNodeType.Element && xmlReaderAssemblyParts.Name == "AssemblyPart")
                                    {
                                        var assemblyPart = new AssemblyPart();
                                        assemblyPart.Source = xmlReaderAssemblyParts.GetAttribute("Source");
                                        assemblyParts.Add(assemblyPart);
                                    }
                                }
                            }

                            break;
                        }
                    }
                }

            }
            return assemblyParts;
        }


        /// <summary>
        /// 加载模块人口类中的模块信息
        /// </summary>
        /// <param name="assemblyParts">The assembly parts.</param>
        protected List<ModuleInfo> GetModuleInfo(IList<AssemblyPart> assemblyParts)
        {
            var moduleList = new List<ModuleInfo>();

            using (var stream = PackageFile.OpenRead())
            {

                var alist = assemblyParts.Select(p => p.Load(
                    Application.GetResourceStream(new StreamResourceInfo(stream, null),
                    new Uri(p.Source, UriKind.Relative)).Stream))
                    //.Where(a => a.FullName.ToLower().StartsWith("zxdframework") || a.FullName.ToLower().Contains(".module")) 
                    .ToList();


                //var catalog = new AggregateCatalog();

                //foreach (var assembly in alist)
                //{
                //    catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                //}

                //var container = new CompositionContainer(catalog);
                

                var clist = alist.SelectMany(a => a.GetExportedTypes()
                                                  .Where(t => t.Name.ToLower().EndsWith("module"))
                                                  .Where(t => t.IsClass)
                                                  .Where(t => !t.IsNestedPublic))
                                                  .ToList();

                moduleList.AddRange(from type in clist
                                    let t = type.GetCustomAttributes(false).Where(o => o.GetType().FullName == "ZxdFramework.DataContract.System.ZxdModuleExportAttribute").ToList()
                                    where t.Count > 0
                                    let info = (ZxdModuleExportAttribute)t[0]
                                    select new ModuleInfo()
                                    {
                                        ModuleName = info.ModuleName,
                                        Author = info.Author,
                                        Description = info.Description,
                                        Ref = PackageFile.Name,
                                        DependsOn = info.DependsOnModuleNames != null ? new Collection<string>(new List<string>(info.DependsOnModuleNames)) : null,
                                        Category = info.Category,
                                        ModuleType = type.AssemblyQualifiedName
                                    });
            }

            return moduleList;
        }

        /// <summary>
        /// Reads the module file.
        /// </summary>
        /// <param name="file">The file.</param>
        protected void ReadModuleFile(FileInfo file)
        {
            var assemblyParts = GetParts(file);
            ModuleList = GetModuleInfo(assemblyParts);

            if (ModuleList.Count > 0)
            {
                //FileData = file.OpenRead();
                using(var stream = file.OpenRead())
                {
                    var temp = new byte[stream.Length];
                    stream.Read(temp, 0, (int)stream.Length);
                    this.FileData = Convert.ToBase64String(temp);
                }

            }
        }
        #endregion
#endif
    }
}