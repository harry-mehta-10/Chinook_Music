using System.Windows.Controls;
using System.Windows.Input;

namespace PROG2500_A2_Chinook.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ArtistsTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow?.NavigateToArtists();
        }

        private void AlbumsTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow?.NavigateToAlbums();
        }

        private void TracksTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow?.NavigateToTracks();
        }

        // Add these to HomePage.xaml.cs
        private void CatalogTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow?.NavigateToMusicCatalog();
        }

        private void CustomerOrdersTile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            mainWindow?.NavigateToCustomerOrders();
        }
    }
}