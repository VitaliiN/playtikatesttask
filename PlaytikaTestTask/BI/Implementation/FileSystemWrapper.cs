using PlaytikaTestTask.BI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlaytikaTestTask.Constants;
using System.IO;

namespace PlaytikaTestTask.BI.Implementation
{
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public async Task<List<string>> GetInnerDirectoriesPaths(string directoryPath)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            DirectoryInfo[] innerDirectories = new DirectoryInfo[0];
            List<string> result = new List<string>();
            try
            {
                await Task.Run(() => innerDirectories = directory.GetDirectories());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return result;
            }
            foreach (var item in innerDirectories)
            {
                try
                {
                    result.Add(item.FullName);
                }
                catch
                {
                    // just skip
                }
            }
            return result;
        }

        public async Task<List<string>> GetFilesNames(string directoryPath, AvailableActions action)
        {
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            List<string> result = new List<string>();
            FileInfo[] files = new FileInfo[0];
            try
            {
                await Task.Run(() =>
                {
                    if (action == AvailableActions.cpp)
                        files = directory.GetFiles(Constant.cppSearchPattern);
                    else
                        files = directory.GetFiles();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            foreach (var file in files)
            {
                result.Add(file.FullName);
            }
            return result;
        }

        public void AppendAllText(string filePath, string text)
        {
            File.AppendAllText(filePath, text + Environment.NewLine);
        }
    }
}
