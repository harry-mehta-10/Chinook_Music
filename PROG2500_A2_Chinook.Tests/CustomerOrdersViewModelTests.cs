using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG2500_A2_Chinook.ViewModels;
using System.Linq;

namespace PROG2500_A2_Chinook.Tests
{
    [TestClass]
    public class CustomerOrdersViewModelTests
    {
        [TestMethod]
        public void CustomerOrdersViewModel_Constructor_LoadsCustomers()
        {
            var viewModel = new CustomerOrdersViewModel();

            // Makes sure loading is done
            System.Threading.Thread.Sleep(2000);

            Assert.IsNotNull(viewModel.Customers);
            Assert.IsTrue(viewModel.Customers.Count > 0);
        }

        [TestMethod]
        public void CustomerOrdersViewModel_CustomerFullName_ShouldBeFormattedCorrectly()
        {
            var viewModel = new CustomerOrdersViewModel();

            System.Threading.Thread.Sleep(2000);

            foreach (var customer in viewModel.Customers)
            {
                // Check that full name is in "LastName, FirstName" format
                Assert.IsTrue(customer.FullName.Contains(", "),
                    $"Full name '{customer.FullName}' is not in expected format");

                var nameParts = customer.FullName.Split(',');
                Assert.AreEqual(2, nameParts.Length,
                    $"Full name '{customer.FullName}' does not split into two parts");

                // Check that both parts are non-empty
                Assert.IsFalse(string.IsNullOrWhiteSpace(nameParts[0]),
                    $"Last name is empty in '{customer.FullName}'");
                Assert.IsFalse(string.IsNullOrWhiteSpace(nameParts[1]),
                    $"First name is empty in '{customer.FullName}'");
            }
        }

        [TestMethod]
        public void CustomerOrdersViewModel_LocationInfo_ShouldNotBeEmpty()
        {
            var viewModel = new CustomerOrdersViewModel();

            // Make sure loading is done
            System.Threading.Thread.Sleep(2000);

            foreach (var customer in viewModel.Customers)
            {
                // Location should never be null or empty
                Assert.IsFalse(string.IsNullOrWhiteSpace(customer.LocationInfo),
                    "LocationInfo should never be null or empty");
            }
        }

        [TestMethod]
        public void CustomerOrdersViewModel_InvoiceDates_ShouldBeOrderedDescending()
        {
            var viewModel = new CustomerOrdersViewModel();

            System.Threading.Thread.Sleep(2000);

            foreach (var customer in viewModel.Customers)
            {
                // Skips customers with 0 or 1 invoice
                if (customer.Invoices.Count <= 1)
                    continue;

                // Check that invoices are ordered by date (descending)
                for (int i = 0; i < customer.Invoices.Count - 1; i++)
                {
                    Assert.IsTrue(customer.Invoices[i].InvoiceDate >= customer.Invoices[i + 1].InvoiceDate,
                        $"Invoices not ordered correctly for customer {customer.FullName}");
                }
            }
        }
    }
}