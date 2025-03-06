namespace Tarea6_Couteau.Views.ContainerViews;

public partial class AcercaDeMi : ContentPage
{
    private const string PortfolioUrl = "https://johnkerlin.com/";
    private const string ImageUrl = "https://firebasestorage.googleapis.com/v0/b/finanzapp-5bbca.firebasestorage.app/o/miimagen.jpg?alt=media&token=23b41ef4-2922-4bcb-9df9-ad00f1bc8152";

    public AcercaDeMi()
    {
        InitializeComponent();
        LoadProfileImage();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProfileImage();

    }
    private async void OnVisitPortfolioClicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(PortfolioUrl);
    }

    private void LoadProfileImage()
    {
        try
        {
            // Asegurar que la URL está bien codificada
            string encodedUrl = Uri.EscapeUriString(ImageUrl);
            ProfileImage.Source = ImageSource.FromUri(new Uri(encodedUrl));
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }
}