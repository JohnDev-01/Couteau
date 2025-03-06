using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tarea6_Couteau.Services
{
    public class ConsultaApi
    {
        private readonly HttpClient _httpClient;

        public ConsultaApi()
        {
            _httpClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");

                string jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en la solicitud HTTP: {ex.Message}");
            }
        }
    }
}
