using System.Linq;
using System.Windows.Controls;
using PROG2500_A2_Chinook.ViewModels;

namespace PROG2500_A2_Chinook.Views
{
    public partial class AlbumsPage : Page
    {
        private AlbumsViewModel _viewModel;

        public AlbumsPage()
        {
            InitializeComponent();
            _viewModel = (AlbumsViewModel)DataContext;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Show all albums if search is empty
                AlbumsListView.ItemsSource = _viewModel.Albums;
            }
            else
            {
                // Filter albums based on search text (title or artist)
                var filteredAlbums = _viewModel.Albums
                    .Where(a => a.Title.ToLower().Contains(searchText) ||
                                a.Artist?.Name.ToLower().Contains(searchText) == true)
                    .ToList();

                AlbumsListView.ItemsSource = filteredAlbums;
            }
        }
    }
}