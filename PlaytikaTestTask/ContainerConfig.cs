using Autofac;
using PlaytikaTestTask.BI.Implementation;
using PlaytikaTestTask.BI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaytikaTestTask
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FileSystemWrapper>().As<IFileSystemWrapper>();
            builder.RegisterType<FileNameTransformerBuilder>().As<IFileNameTransformerBuilder>();
            builder.RegisterType<DirectoryService>().As<IDirectoryService>();

            return builder.Build();
        }
    }
}
