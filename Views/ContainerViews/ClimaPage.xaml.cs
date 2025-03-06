using Tarea6_Couteau.Services;

namespace Tarea6_Couteau.Views.ContainerViews;

public partial class ClimaPage : ContentPage
{
    private readonly ConsultaApi _apiService;
    private const string API_KEY = "8eb3806783f46e3f341f74d39b076b63";
    private const string CITY = "Santo Domingo";
    private const string COUNTRY = "DO";

    public ClimaPage()
    {
        InitializeComponent();
        _apiService = new ConsultaApi();
        DateLabel.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        GetWeather();
    }

    private async void OnGetWeatherClicked(object sender, EventArgs e)
    {
        await GetWeather();
    }

    private async Task GetWeather()
    {
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={CITY},{COUNTRY}&appid={API_KEY}&units=metric&lang=es";

        try
        {
            var weatherData = await _apiService.GetAsync<WeatherResponse>(apiUrl);

            if (weatherData != null)
            {
                TemperatureLabel.Text = $"{weatherData.Main.Temp}�C";
                DescriptionLabel.Text = weatherData.Weather[0].Description.ToUpper();
                var urlClima = $"https://openweathermap.org/img/wn/{weatherData.Weather[0].Icon}@2x.png";
                WeatherIcon.Source = ImageSource.FromUri(new Uri(urlClima));

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener el clima: {ex.Message}", "OK");
        }
    }

    public class WeatherResponse
    {
        public WeatherMain Main { get; set; }
        public WeatherDescription[] Weather { get; set; }
    }

    public class WeatherMain
    {
        public float Temp { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
