using System;
using PlaytikaTestTask.Constants;
using System.IO;
using PlaytikaTestTask.BI.Interfaces;
using Autofac;

namespace PlaytikaTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string destFileName = Constant.destinationFileName;
            if (!ValidateParams(args))
            {
                Console.ReadKey();
                return;
            }
            if (args.Length == 3)
                destFileName = args[2];
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var directoryService = scope.Resolve<IDirectoryService>();
                directoryService.RootFolder = args[0].Replace('/', '\\');
                if (!directoryService.RootFolder.EndsWith("\\"))
                    directoryService.RootFolder += "\\";
                try
                {
                    directoryService.Scan(args[0], Constant.AvailableActionsDictionary[args[1]], destFileName).GetAwaiter().GetResult();
                    Console.WriteLine("Finished");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        #region Validation

        private static bool ValidateParams(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("should be at least 2 parameters... pls try again");
                return false;
            }
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("directory for scan does not exist... pls try again");
            }
            if (!Constant.AvailableActionsDictionary.ContainsKey(args[1]))
            {
                Console.WriteLine("wrong action name, it can be - all,cpp,reversed1,reversed2... pls try again");
                return false;
            }
            string destFileName = Constant.destinationFileName;
            if (args.Length == 3)
            {
                destFileName = args[2];
            }
            try
            {
                File.WriteAllText(destFileName, String.Empty);
            }
            catch
            {
                Console.WriteLine("couldn't create destination file name");
                return false;
            }
            return true;
        }
        #endregion
    }
}
