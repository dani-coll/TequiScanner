using TequiScanner.Shared.Model;
using System.Threading.Tasks;
using TequiScanner.Shared.Services.Intefaces;
using Autofac;
using Autofac.Core;

namespace TequiScanner.Shared.Services
{
    // TODO HTTP request to API
    public class StorageService : IStorageService
    {
        private IComputerVisionService _computerVisionService;

        public StorageService(IComputerVisionService computerVisionService)
        {
            _computerVisionService = computerVisionService;
        }

        public Task<RecognitionResult> ReadFromLocal(string imagePath)
        {
            if (_computerVisionService == null) throw new DependencyResolutionException($"{typeof(IComputerVisionService)} not registered.");

        }

        public Task<RecognitionResult> ReadFromLocal(byte[] imageBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}