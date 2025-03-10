using System.Linq;
using System.Windows.Controls;
using PROG2500_A2_Chinook.ViewModels;

namespace PROG2500_A2_Chinook.Views
{
    public partial class ArtistsPage : Page
    {
        private ArtistsViewModel _viewModel;

        public ArtistsPage()
        {
            InitializeComponent();
            _viewModel = (ArtistsViewModel)DataContext;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all artists if search is empty
                ArtistsListView.ItemsSource = _viewModel.Artists;
            }
            else
            {
                // Filter artists based on search text
                var filteredArtists = _viewModel.Artists
                    .Where(a => a.Name.ToLower().Contains(searchText))
                    .ToList();

                ArtistsListView.ItemsSource = filteredArtists;
            }
        }
    }
}