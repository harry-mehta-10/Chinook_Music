using PROG2500_A2_Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PROG2500_A2_Chinook.ViewModels
{
    public class AlbumsViewModel : BaseViewModel
    {
        private ObservableCollection<Album> _albums;
        public ObservableCollection<Album> Albums
        {
            get { return _albums ?? (_albums = new ObservableCollection<Album>()); }
            set { SetProperty(ref _albums, value); }
        }

        public AlbumsViewModel()
        {
            _albums = new ObservableCollection<Album>();
            LoadAlbums();
        }

        private void LoadAlbums()
        {
            IsLoading = true;

            try
            {
                using (var context = new ChinookContext())
                {
                    var albumList = context.Albums
                        .Include(a => a.Artist)
                        .OrderBy(a => a.Title)
                        .ToList();

                    Albums = new ObservableCollection<Album>(albumList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading albums: {ex.Message}",
                               "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}