using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace onlinebankingapp.Utilities
{
    public interface IApiWrapper
    {
        Task<HttpResponseMessage> DeleteAPI(string token, string apiUrl, string contentData);
        Task<HttpResponseMessage> GetAPI(string token, string apiUrl);
         Task<HttpResponseMessage>  PostAPI(string token, string apiUrl, string contentData);
        Task<HttpResponseMessage> PostFormDataAPI(string token, string apiUrl, IList<KeyValuePair<string, string>> contentData);
        Task<HttpResponseMessage> PutAPI(string token, string apiUrl, string contentData);
        Task<HttpResponseMessage> SimpleGetAPI(string apiUrl);
        Task<HttpResponseMessage> SimplePostFormDataAPI(string apiUrl, IList<KeyValuePair<string, string>> contentData);
    }
}