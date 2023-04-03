
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Services
{
    public class RingtoneService : IRingtoneService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RingtoneService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<RingtoneResponseDto>> GetAsync()
        {
            try
            {
                var http = _httpClientFactory.CreateClient("smrtbell");
                var response = await http.GetAsync("ringtones");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<RingtoneResponseDto>>();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
