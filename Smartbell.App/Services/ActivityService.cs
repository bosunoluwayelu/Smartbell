﻿using Smartbell.Shared.Contracts;

namespace Smartbell.App.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ActivityService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActivityResponseDto> CreateAsync(CreateActivityDto entity)
        {
            var http = _httpClientFactory.CreateClient("smrtbell");
            var response = await http.PostAsJsonAsync("activities", entity);
            return await response.Content.ReadFromJsonAsync<ActivityResponseDto>();
        }

        public Task<ActivityResponseDto> DeleteAsync(ConfigResponseDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActivityResponseDto>> GetAsync()
        {
            var http = _httpClientFactory.CreateClient("smrtbell");
            var response = await http.GetAsync("activities");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ActivityResponseDto>>();
            }

            return null;
        }

        public Task<ActivityResponseDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
