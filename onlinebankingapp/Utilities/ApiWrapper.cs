using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace onlinebankingapp.Utilities
{
    public class ApiWrapper : IApiWrapper
    {
        public async Task<HttpResponseMessage> GetAPI(string token, string apiUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);

            return response;
        }
        public async Task<HttpResponseMessage> SimpleGetAPI(string apiUrl)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);

            return response;
        }

        public async Task<HttpResponseMessage> PostAPI(string token, string apiUrl, string contentData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            request.Content = new StringContent(contentData, Encoding.UTF8, "application/json");
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);
            return response;
        }

        public async Task<HttpResponseMessage> PostFormDataAPI(string token, string apiUrl, IList<KeyValuePair<string, string>> contentData)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new MultipartFormDataContent();
            foreach (var keyValuePair in contentData)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }
            var response = await client.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<HttpResponseMessage> SimplePostFormDataAPI(string apiUrl, IList<KeyValuePair<string, string>> contentData)
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var content = new MultipartFormDataContent();
            foreach (var keyValuePair in contentData)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }
            var response = await client.PostAsync(apiUrl, content);
            return response;
        }
        public async Task<HttpResponseMessage> PutAPI(string token, string apiUrl, string contentData)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, apiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(contentData, Encoding.UTF8, "application/json");
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);
            return response;
        }
        public async Task<HttpResponseMessage> DeleteAPI(string token, string apiUrl, string contentData)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = new StringContent(contentData, Encoding.UTF8, "application/json");
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var client = new HttpClient(handler);
            var response = await client.SendAsync(request);
            return response;
        }
    }
}
