using System.Windows.Controls;
using PROG2500_A2_Chinook.ViewModels;

namespace PROG2500_A2_Chinook.Views
{
    public partial class CustomerOrdersPage : Page
    {
        public CustomerOrdersPage()
        {
            InitializeComponent();
            this.DataContext = new CustomerOrdersViewModel();
        }
    }
}