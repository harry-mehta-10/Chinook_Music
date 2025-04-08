using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG2500_A2_Chinook.ViewModels;
using System.Linq;

namespace PROG2500_A2_Chinook.Tests
{
    [TestClass]
    public class MusicCatalogViewModelTests
    {
        [TestMethod]
        public void MusicCatalogViewModel_Constructor_LoadsArtistGroups()
        {
            var viewModel = new MusicCatalogViewModel();

            System.Threading.Thread.Sleep(2000);

            Assert.IsNotNull(viewModel.ArtistGroups);
            Assert.IsTrue(viewModel.ArtistGroups.Count > 0);
        }

        [TestMethod]
        public void MusicCatalogViewModel_ArtistGroups_ShouldBeOrderedByGroupKey()
        {
            var viewModel = new MusicCatalogViewModel();

            System.Threading.Thread.Sleep(2000);

            var groups = viewModel.ArtistGroups.ToList();

            for (int i = 0; i < groups.Count - 1; i++)
            {
                Assert.IsTrue(string.Compare(groups[i].GroupKey, groups[i + 1].GroupKey) <= 0,
                    $"Artist groups not ordered correctly at positions {i} and {i + 1}");
            }
        }

        [TestMethod]
        public void MusicCatalogViewModel_ArtistCount_ShouldMatchNumberOfArtistsInGroup()
        {
            var viewModel = new MusicCatalogViewModel();

            System.Threading.Thread.Sleep(2000);

            foreach (var group in viewModel.ArtistGroups)
            {
                Assert.AreEqual(group.Artists.Count, group.ArtistCount,
                    $"Group {group.GroupKey} has ArtistCount {group.ArtistCount} but actual count is {group.Artists.Count}");
            }
        }
    }
}