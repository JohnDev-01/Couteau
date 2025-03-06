using System.Collections.ObjectModel;
using Tarea6_Couteau.Services;

namespace Tarea6_Couteau.Views.ContainerViews;

public partial class WordpressPage : ContentPage
{
    private readonly ConsultaApi _apiService;
    public ObservableCollection<NewsItem> News { get; set; }

    private const string WordPressApiUrl = "https://kinsta.com/wp-json/wp/v2/posts?per_page=3";
    private const string WordPressLogoUrl = "https://kinsta.com/wp-content/uploads/2019/01/cropped-kinsta-favicon-192x192.png";

    public WordpressPage()
    {
        InitializeComponent();
        _apiService = new ConsultaApi();
        News = new ObservableCollection<NewsItem>();
        NewsCollectionView.ItemsSource = News;

        WebsiteLogo.Source = ImageSource.FromUri(new Uri(WordPressLogoUrl));
        WebsiteLogo.IsVisible = true;
    }

    private async void OnFetchNewsClicked(object sender, EventArgs e)
    {
        try
        {
            var posts = await _apiService.GetAsync<WordPressPost[]>(WordPressApiUrl);

            News.Clear();
            foreach (var post in posts)
            {
                News.Add(new NewsItem
                {
                    Title = post.Title.Rendered,
                    Excerpt = StripHtml(post.Excerpt.Rendered),
                    Link = post.Link
                });
            }

            NewsCollectionView.IsVisible = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar las noticias: {ex.Message}", "OK");
        }
    }

    private async void OnVisitNews(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string url)
        {
            await Launcher.OpenAsync(url);
        }
    }

    private string StripHtml(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty);
    }

    public class WordPressPost
    {
        public PostTitle Title { get; set; }
        public PostExcerpt Excerpt { get; set; }
        public string Link { get; set; }
    }

    public class PostTitle
    {
        public string Rendered { get; set; }
    }

    public class PostExcerpt
    {
        public string Rendered { get; set; }
    }

    public class NewsItem
    {
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Link { get; set; }
    }
}