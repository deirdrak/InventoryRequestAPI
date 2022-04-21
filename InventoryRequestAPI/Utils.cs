using Microsoft.AspNetCore.Mvc;

namespace InventoryRequestAPI
{
    public interface IUtils
    {
        Task<HttpResponseMessage> ApiCallAsync(string apiUrl);
    }
    public class Utils:IUtils
    {
        public async Task<HttpResponseMessage> ApiCallAsync(string apiUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var apiURL = apiUrl;

                var response = await httpClient.GetAsync(apiURL);
                    
                return response;                                        
            }           
        }
    }
}
