using PROG2500_A2_Chinook.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PROG2500_A2_Chinook.ViewModels
{
    public class ArtistsViewModel : BaseViewModel
    {
        private ObservableCollection<Artist> _artists;
        public ObservableCollection<Artist> Artists
        {
            get { return _artists ?? (_artists = new ObservableCollection<Artist>()); }
            set { SetProperty(ref _artists, value); }
        }

        public ArtistsViewModel()
        {
            _artists = new ObservableCollection<Artist>();
            LoadArtists();
        }

        private void LoadArtists()
        {
            IsLoading = true;

            try
            {
                using (var context = new ChinookContext())
                {
                    var artistList = context.Artists.OrderBy(a => a.Name).ToList();
                    Artists = new ObservableCollection<Artist>(artistList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading artists: {ex.Message}",
                               "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}