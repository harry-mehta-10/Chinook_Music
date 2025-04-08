using PROG2500_A2_Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PROG2500_A2_Chinook.ViewModels
{
    public class MusicCatalogViewModel : BaseViewModel
    {
        private ObservableCollection<ArtistGrouping> _artistGroups;
        public ObservableCollection<ArtistGrouping> ArtistGroups
        {
            get { return _artistGroups ?? (_artistGroups = new ObservableCollection<ArtistGrouping>()); }
            set { SetProperty(ref _artistGroups, value); }
        }

        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    // Reloads data with filter applied
                    LoadCatalogData();
                }
            }
        }

        private int _totalArtistsCount;
        public int TotalArtistsCount
        {
            get { return _totalArtistsCount; }
            set { SetProperty(ref _totalArtistsCount, value); }
        }

        private int _totalAlbumsCount;
        public int TotalAlbumsCount
        {
            get { return _totalAlbumsCount; }
            set { SetProperty(ref _totalAlbumsCount, value); }
        }

        public MusicCatalogViewModel()
        {
            IsLoading = true;
            LoadCatalogData();
        }

        public void LoadCatalogData()
        {
            IsLoading = true;

            try
            {
                using (var context = new ChinookContext())
                {
                    TotalArtistsCount = context.Artists.Count();
                    TotalAlbumsCount = context.Albums.Count();

                    // Makes sure to eagerly load all related data
                    var artistsQuery = context.Artists
                        .Include(a => a.Albums)
                            .ThenInclude(alb => alb.Tracks)
                        .AsQueryable();

                    // Applies search filter if provided
                    if (!string.IsNullOrWhiteSpace(SearchText))
                    {
                        string searchLower = SearchText.ToLower();
                        artistsQuery = artistsQuery.Where(a =>
                            a.Name.ToLower().Contains(searchLower) ||
                            a.Albums.Any(alb =>
                                alb.Title.ToLower().Contains(searchLower) ||
                                alb.Tracks.Any(t => t.Name.ToLower().Contains(searchLower))
                            )
                        );
                    }

                    var allArtists = artistsQuery.ToList();

                    // Group artists by first letter
                    var groupedArtists = allArtists
                        .GroupBy(a => !string.IsNullOrEmpty(a.Name) ? a.Name.Substring(0, 1).ToUpper() : "?")
                        .OrderBy(g => g.Key)
                        .Select(g => new ArtistGrouping
                        {
                            GroupKey = g.Key,
                            Artists = new ObservableCollection<Artist>(g.OrderBy(a => a.Name)),
                            ArtistCount = g.Count()
                        })
                        .ToList();

                    // Updates the observable collection on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ArtistGroups = new ObservableCollection<ArtistGrouping>(groupedArtists);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading music catalog: {ex.Message}",
                                "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    public class ArtistGrouping
    {
        public string GroupKey { get; set; } = "";
        public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();
        public int ArtistCount { get; set; }
    }
}