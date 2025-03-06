using Tarea6_Couteau.Services;

namespace Tarea6_Couteau.Views.ContainerViews;

public partial class GeneroPage : ContentPage
{
    private readonly ConsultaApi _apiService;

    public GeneroPage()
	{
		InitializeComponent();
        _apiService = new ConsultaApi();
        BindingContext = this;
    }


    private async void OnPredictGenderClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Error", "Por favor, ingrese un nombre válido.", "OK");
            return;
        }

        string apiUrl = $"https://api.genderize.io/?name={name}";

        try
        {
            var genderResponse = await _apiService.GetAsync<GenderResponse>(apiUrl);

            if (genderResponse?.Gender == "male")
            {
                BackgroundColor = Color.FromArgb("#2196F3"); 
                ResultLabel.Text = $"El género predicho para {name} es Masculino";
            }
            else if (genderResponse?.Gender == "female")
            {
                BackgroundColor = Color.FromArgb("#E91E63"); // Rosa para femenino
                ResultLabel.Text = $"El género predicho para {name} es Femenino";
            }
            else
            {
                BackgroundColor = Color.FromArgb("#757575"); // Gris si no se encuentra
                ResultLabel.Text = $"No se pudo determinar el género de {name}";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema con la predicción: {ex.Message}", "OK");
        }
    }
}
public class GenderResponse
{
    public string Gender { get; set; }
}