using PlaytikaTestTask.BI.Interfaces;
using PlaytikaTestTask.Constants;
using System;
using System.Threading.Tasks;

namespace PlaytikaTestTask.BI.Implementation
{
    public class DirectoryService : IDirectoryService
    {
        #region Private variables

        private readonly IFileSystemWrapper fileSystemWrapper;
        private readonly IFileNameTransformerBuilder fileNameTransformerBuilder;

        #endregion
        public DirectoryService(IFileSystemWrapper fileSystemWrapper, IFileNameTransformerBuilder fileNameTransformerBuilder)
        {
            this.fileSystemWrapper = fileSystemWrapper;
            this.fileNameTransformerBuilder = fileNameTransformerBuilder;
        }

        /// <summary>
        /// selected base folder path
        /// </summary>
        public string RootFolder { get; set; }

        /// <summary>
        /// recursive scan of root and all inner folders
        /// </summary>
        /// <param name="sourceFolder">current folder</param>
        /// <param name="actionType"></param>
        /// <param name="destinationFileName">result file path</param>
        /// <returns></returns>
        public async Task Scan(string sourceFolder, AvailableActions actionType, string destinationFileName)
        {
            var fileNameTransformer = fileNameTransformerBuilder.GetTransformer(actionType);
            var fileNames = await fileSystemWrapper.GetFilesNames(sourceFolder, actionType);
            if (fileNames != null)
            {
                foreach (var fileName in fileNames)
                {
                    try
                    {
                        fileSystemWrapper.AppendAllText(destinationFileName,
                            fileNameTransformer.Transform(fileName.Replace(RootFolder, string.Empty)));
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            var innerFolders = await fileSystemWrapper.GetInnerDirectoriesPaths(sourceFolder);
            if (innerFolders != null)
            {
                foreach (var innerFolder in innerFolders)
                {
                    await Scan(innerFolder, actionType, destinationFileName);
                }
            }
        }
    }
}
