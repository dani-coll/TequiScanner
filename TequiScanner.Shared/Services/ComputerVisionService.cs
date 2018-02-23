using TequiScanner.Shared.Model;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using TequiScanner.Shared.Services.Intefaces;
using System.Net.Http.Headers;
using TequiScanner.Shared.Constants;
using System.Linq;

namespace TequiScanner.Shared.Services
{
    public class ComputerVisionService : IComputerVisionService
    {
        #region API

        /// <summary>
        /// The key.
        /// Second key: 3da74e8f94b24ac5b6e9c797f633e7fa
        /// </summary>
        private readonly string _key = "f183c0b7195c4a6ea22efee295faa6e5";

        /// <summary>
        /// Documentation for the API: https://www.microsoft.com/cognitive-services/en-us/computer-vision-api
        /// </summary>
        private readonly string _analyseImageUri = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/recognizeText?handwriting=true";

        #endregion

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content. 
        /// </summary>
        /// <returns></returns>
        public async Task<RecognitionResult> RecognizeTextService(byte[] imageBytes)
        {
            var httpClient = BuildClient();
            var byteContent = new ByteArrayContent(imageBytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var response = await httpClient.PostAsync(_analyseImageUri, byteContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var operationLocationRequest = GetOperationLocation(response.Headers);

                    var getReponse = await httpClient.GetAsync(operationLocationRequest);
                    var jsonResult = getReponse.Content.ReadAsStringAsync().Result;

                    var analyticsResponse = JsonConvert.DeserializeObject<AnalyticsResponse>(jsonResult);

                    return analyticsResponse.Status.Equals("Succeeded")
                        ? analyticsResponse.RecognitionResult
                        : null;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
                throw exception;
            }
        }

        private string GetOperationLocation(HttpResponseHeaders headers)
        {
            return headers.ToDictionary(l => l.Key, k => k.Value)[ApiHeaders.OperationLocation].First();
        }

        private HttpClient BuildClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(ApiHeaders.SubscriptionKey, _key);
            return client;
        }
    }
}