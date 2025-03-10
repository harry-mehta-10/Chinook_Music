using System.Linq;
using System.Windows.Controls;
using PROG2500_A2_Chinook.ViewModels;

namespace PROG2500_A2_Chinook.Views
{
    public partial class TracksPage : Page
    {
        private TracksViewModel _viewModel;

        public TracksPage()
        {
            InitializeComponent();
            _viewModel = (TracksViewModel)DataContext;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all tracks if search is empty
                TracksListView.ItemsSource = _viewModel.Tracks;
            }
            else
            {
                // Filter tracks based on search text
                var filteredTracks = _viewModel.Tracks
                    .Where(t =>
                        t.Name.ToLower().Contains(searchText) ||
                        t.Album?.Title.ToLower().Contains(searchText) == true ||
                        t.Album?.Artist?.Name.ToLower().Contains(searchText) == true ||
                        t.Composer?.ToLower().Contains(searchText) == true)
                    .ToList();

                TracksListView.ItemsSource = filteredTracks;
            }
        }

        private void TracksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Show details when an item is selected
            if (TracksListView.SelectedItem != null)
            {
                DetailsExpander.Visibility = System.Windows.Visibility.Visible;
                DetailsExpander.IsExpanded = true;
            }
            else
            {
                DetailsExpander.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}