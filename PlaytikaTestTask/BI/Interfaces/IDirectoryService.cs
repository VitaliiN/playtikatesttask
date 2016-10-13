using PlaytikaTestTask.Constants;
using System.Threading.Tasks;

namespace PlaytikaTestTask.BI.Interfaces
{
    public interface IDirectoryService
    {
        string RootFolder { get; set; }
        Task Scan(string sourceFolder, AvailableActions actionType, string destinationFileName);
    }
}
