using PROG2500_A2_Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PROG2500_A2_Chinook.ViewModels
{
    public class TracksViewModel : BaseViewModel
    {
        private ObservableCollection<Track> _tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return _tracks ?? (_tracks = new ObservableCollection<Track>()); }
            set { SetProperty(ref _tracks, value); }
        }

        public TracksViewModel()
        {
            _tracks = new ObservableCollection<Track>();
            LoadTracks();
        }

        private void LoadTracks()
        {
            IsLoading = true;

            try
            {
                using (var context = new ChinookContext())
                {
                    var trackList = context.Tracks
                        .Include(t => t.Album)
                        .ThenInclude(a => a.Artist)
                        .Include(t => t.MediaType)
                        .Include(t => t.Genre)
                        .OrderBy(t => t.Name)
                        .Take(100) 
                        .ToList();

                    Tracks = new ObservableCollection<Track>(trackList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tracks: {ex.Message}",
                               "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}