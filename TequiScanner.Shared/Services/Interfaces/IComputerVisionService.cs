using TequiScanner.Shared.Model;
using System.Threading.Tasks;

namespace TequiScanner.Shared.Services.Intefaces
{
    public interface IComputerVisionService
    {
        Task<AnalyticsResponse> RecognizeTextService(byte[] imageBytes);
    }
}