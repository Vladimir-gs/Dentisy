using Shared.DTOs;
using System.Net.Http.Json;

namespace Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DentisyApi");
        }

        public async Task<AuthResponseDTO?> LoginAsync(LoginDTO loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
            }

            return null;
        }

        public async Task<AuthResponseDTO?> RegisterAsync(RegisterDTO registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/register", registerDto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AuthResponseDTO>();
            }

            return null;
        }
    }
}
