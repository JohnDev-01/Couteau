using System.Collections.ObjectModel;
using Tarea6_Couteau.Services;

namespace Tarea6_Couteau.Views.ContainerViews;

public partial class UniversidadPage : ContentPage
{

    private readonly ConsultaApi _apiService;
    public ObservableCollection<University> Universities { get; set; }

    public UniversidadPage()
    {
        InitializeComponent();
        _apiService = new ConsultaApi();
        Universities = new ObservableCollection<University>();
        BindingContext = this;
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string country = CountryEntry.Text?.Trim();
        if (string.IsNullOrEmpty(country))
        {
            await DisplayAlert("Error", "Por favor, ingrese un país en inglés.", "OK");
            return;
        }

        string formattedCountry = country.Replace(" ", "+");
        string apiUrl = $"http://universities.hipolabs.com/search?country={formattedCountry}";

        try
        {
            var universities = await _apiService.GetAsync<University[]>(apiUrl);

            Universities.Clear();
            foreach (var uni in universities)
            {
                Universities.Add(new University
                {
                    Name = uni.Name,
                    Domain = uni.Domains?.FirstOrDefault() ?? "No disponible",
                    WebPage = uni.web_pages?.FirstOrDefault() ?? "#"
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al obtener las universidades: {ex.Message}", "OK");
        }
    }

    private async void OnVisitWebsite(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string url && url != "#")
        {
            await Launcher.OpenAsync(url);
        }
    }

    public class University
    {
        public string Name { get; set; } // Mapea 'name'
        public string[] Domains { get; set; } // Mapea 'domains'
        public string[] web_pages { get; set; } // Mapea 'web_pages'
        public string Domain { get; set; } // Extrae el primer dominio
        public string WebPage { get; set; } // Extrae el primer enlace web
    }
}