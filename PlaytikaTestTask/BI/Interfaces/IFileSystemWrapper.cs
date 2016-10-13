using PlaytikaTestTask.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaytikaTestTask.BI.Interfaces
{
    public interface IFileSystemWrapper
    {
        Task<List<string>> GetFilesNames(string directoryPath, AvailableActions action);
        Task<List<string>> GetInnerDirectoriesPaths(string directoryPath);
        void AppendAllText(string filePath, string text);



    }
}
