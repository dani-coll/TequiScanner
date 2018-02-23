using TequiScanner.Shared.Model;
using System.Threading.Tasks;
using TequiScanner.Shared.Services.Intefaces;

namespace TequiScanner.Shared.Services
{
    // TODO HTTP request to API
    public class StorageService : IStorageService
    {
        public Task<AnalyticsResponse> ReadFromLocal(string imagePath)
        {
            throw new System.NotImplementedException();
        }

        public Task<AnalyticsResponse> ReadFromLocal(byte[] imageBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}