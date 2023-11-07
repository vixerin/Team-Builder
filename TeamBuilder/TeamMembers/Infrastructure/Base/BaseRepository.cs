using System.Text;
using System.Text.Json;
using TeamBuilder.Common.Functional;

namespace TeamBuilder.TeamMembers.Infrastructure.Base
{
    public abstract class BaseRepository
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
                    var result = JsonSerializer.Deserialize<ResultDto<T>>(content, _serializerOptions);
                    return result.FromDto();
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
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_url}/{endpointUri}", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<ResultDto>(content, _serializerOptions);
                    return result.FromDto();
                }

                return Result.Fail($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
