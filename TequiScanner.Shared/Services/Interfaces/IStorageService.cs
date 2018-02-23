using TequiScanner.Shared.Model;
using System.Threading.Tasks;

namespace TequiScanner.Shared.Services.Intefaces
{
    public interface IStorageService
    {
        Task<AnalyticsResponse> ReadFromLocal(string imagePath);
        Task<AnalyticsResponse> ReadFromLocal(byte[] imageBytes);
    }
}