using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Smartbell.App.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ConfigService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ConfigResponseDto> CreateAsync(CreateConfigDto entity)
        {
			var http = _httpClientFactory.CreateClient("smrtbell");
            var response = await http.PostAsJsonAsync("configs", entity);
			return await response.Content.ReadFromJsonAsync<ConfigResponseDto>();
		}

		public async Task<ConfigResponseDto> UpdateAsync(ConfigResponseDto entity)
		{
            try
            {
				var http = _httpClientFactory.CreateClient("smrtbell");
                var jsonData = JsonConvert.SerializeObject(entity);
				var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var response = await http.PatchAsync($"configs/{entity.Id}", stringContent);
				return await response.Content.ReadFromJsonAsync<ConfigResponseDto>();
			}
            catch (Exception ex)
            {
                throw;
            }
            return null;
		}

		public async Task<ConfigResponseDto> DeleteAsync(ConfigResponseDto entity)
        {
			var http = _httpClientFactory.CreateClient("smrtbell");
            var response = await http.PostAsJsonAsync($"configs/{entity.Id}", entity);
			return await response.Content.ReadFromJsonAsync<ConfigResponseDto>();
		}

        public async Task<IEnumerable<ConfigResponseDto>> GetAsync()
        {
            try
            {
                var http = _httpClientFactory.CreateClient("smrtbell");
                var response = await http.GetAsync("configs");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<ConfigResponseDto>>();
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ConfigResponseDto> GetByIdAsync(Guid id)
        {
            try
            {
                var http = _httpClientFactory.CreateClient("smrtbell");
                var response = await http.GetAsync($"configs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ConfigResponseDto>();
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
