using PROG2500_A2_Chinook.Views;
using System.Windows;

namespace PROG2500_A2_Chinook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Start with the home page when the application launches
            MainFrame.Navigate(new HomePage());
        }

        // Menu click handlers
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void ArtistsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ArtistsPage());
        }

        private void AlbumsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AlbumsPage());
        }

        private void TracksMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TracksPage());
        }

        // Toolbar button click handlers
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ArtistsPage());
        }

        private void AlbumsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AlbumsPage());
        }
        public void NavigateToHome()
        {
            MainFrame.Navigate(new HomePage());
        }

        public void NavigateToArtists()
        {
            MainFrame.Navigate(new ArtistsPage());
        }

        public void NavigateToAlbums()
        {
            MainFrame.Navigate(new AlbumsPage());
        }

        public void NavigateToTracks()
        {
            MainFrame.Navigate(new TracksPage());
        }
        private void TracksButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TracksPage());
        }
    }
}


