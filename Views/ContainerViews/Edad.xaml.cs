using Tarea6_Couteau.Services;

namespace Tarea6_Couteau.Views.ContainerViews;

public partial class Edad : ContentPage
{

    private readonly ConsultaApi _apiService;

    public Edad()
    {
        InitializeComponent();
        _apiService = new ConsultaApi();
        BindingContext = this;
    }

    private async void OnPredictAgeClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Error", "Por favor, ingrese un nombre válido.", "OK");
            return;
        }

        string apiUrl = $"https://api.agify.io/?name={name}";

        try
        {
            var ageResponse = await _apiService.GetAsync<AgeResponse>(apiUrl);

            if (ageResponse?.Age == null)
            {
                await DisplayAlert("Error", "No se pudo determinar la edad.", "OK");
                return;
            }

            int age = ageResponse.Age.Value;
            string category;
            string imageUrl;

            if (age < 18)
            {
                category = "Joven 🧒";
                imageUrl = "boy.png";
            }
            else if (age < 60)
            {
                category = "Adulto 👨‍💼";
                imageUrl = "couple.png"; // Reemplaza con la imagen correspondiente
            }
            else
            {
                category = "Anciano 👴";
                imageUrl = "don.png"; // Reemplaza con la imagen correspondiente
            }

            ResultLabel.Text = $"Edad estimada de {name}: {age} años ({category})";
            PersonImage.Source = ImageSource.FromFile(imageUrl);
            PersonImage.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema con la predicción: {ex.Message}", "OK");
        }
    }
    private class AgeResponse
    {
        public int? Age { get; set; }
    }
}