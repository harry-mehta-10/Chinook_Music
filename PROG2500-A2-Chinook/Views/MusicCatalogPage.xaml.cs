using System.Windows.Controls;
using PROG2500_A2_Chinook.ViewModels;

namespace PROG2500_A2_Chinook.Views
{
    public partial class MusicCatalogPage : Page
    {
        public MusicCatalogPage()
        {
            InitializeComponent();
            this.DataContext = new MusicCatalogViewModel();
        }
    }
}