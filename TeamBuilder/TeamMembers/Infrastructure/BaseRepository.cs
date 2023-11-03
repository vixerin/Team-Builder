using System.Text;
using System.Text.Json;
using TeamBuilder.Functional;

namespace TeamBuilder.TeamMembers.Infrastructure
{
    public class BaseRepository
    {
        private readonly string _url;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly HttpClient _httpClient;

        protected BaseRepository()
        {
            _url = DeviceInfo.Platform == DevicePlatform.Android
                ? "https://10.0.2.2:7222/api/v1/Team"
                : "https://localhost:7222/api/v1/Team";

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _httpClient = new HttpClient(GetInsecureHandler());
        }

        private HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (_, cert, _, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        protected async Task<Result<T>> GetAsync<T>(string endpointUri) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_url}/{endpointUri}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    return Result.Ok(data);
                }

                return Result.Fail<T>($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex.Message);
            }
        }

        protected async Task<Result> PostAsync<T>(string endpointUri, T data) where T: class
        {
            try
            {
                string json = JsonSerializer.Serialize(data, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}/{endpointUri}", content);

                return response.IsSuccessStatusCode ? Result.Ok() : Result.Fail($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
