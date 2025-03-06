using System.Threading.Tasks;

namespace Tarea6_Couteau.Views;

public partial class Presentation : ContentPage
{
	public Presentation()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        await imagenHerramientas.FadeTo(1, 500);
        await imagenHerramientas.FadeTo(0, 500);
        await imagenHerramientas.FadeTo(1, 500);
        await imagenHerramientas.FadeTo(0, 500);
        await imagenHerramientas.FadeTo(1, 500);
        Application.Current.MainPage = new ContainerViews.Container();
    }
}