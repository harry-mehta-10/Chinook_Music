using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG2500_A2_Chinook.ViewModels;
using System.Linq;

namespace PROG2500_A2_Chinook.Tests
{
    [TestClass]
    public class ArtistsViewModelTests
    {
        [TestMethod]
        public void ArtistsViewModel_Constructor_LoadsArtists()
        {
            // Arrange & Act
            var viewModel = new ArtistsViewModel();

            // Assert
            Assert.IsNotNull(viewModel.Artists);
            Assert.IsTrue(viewModel.Artists.Count > 0);
        }

        [TestMethod]
        public void ArtistsViewModel_Artists_ShouldBeOrderedByName()
        {
            // Arrange
            var viewModel = new ArtistsViewModel();

            // Act
            var artists = viewModel.Artists.ToList();

            // Assert
            for (int i = 0; i < artists.Count - 1; i++)
            {
                Assert.IsTrue(string.Compare(artists[i].Name, artists[i + 1].Name) <= 0,
                    $"Artists not ordered correctly at positions {i} and {i + 1}");
            }
        }
    }
}