
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class WeatherResult
{
    public List<CurrentCondition>? current_condition { get; set; }
}

public class CurrentCondition
{
    public string? temp_C { get; set; }
    public List<ValueText>? weatherDesc { get; set; }
}

public class ValueText
{
    public string? value { get; set; }
}

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResult?> GetWeatherDataAsync(string city)
    {
        try
        {
            var url = $"https://wttr.in/{city}?format=j1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResult>(json);
        }
        catch
        {
            return null;
        }
    }
}
