using TequiScanner.Shared.Model;
namespace TequiScanner.Shared.Services
{
    public interface IStorageService
    {
        AnalyticsResponse ReadFromLocal(string imagePath);
        AnalyticsResponse ReadFromLocal(byte[] imageBytes);
    }
}
