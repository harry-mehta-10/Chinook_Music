using PROG2500_A2_Chinook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PROG2500_A2_Chinook.ViewModels
{
    public class CustomerOrdersViewModel : BaseViewModel
    {
        private ObservableCollection<CustomerWithInvoices> _customers;
        public ObservableCollection<CustomerWithInvoices> Customers
        {
            get { return _customers ?? (_customers = new ObservableCollection<CustomerWithInvoices>()); }
            set { SetProperty(ref _customers, value); }
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
                    LoadCustomerData();
                }
            }
        }

        private int _totalCustomersCount;
        public int TotalCustomersCount
        {
            get { return _totalCustomersCount; }
            set { SetProperty(ref _totalCustomersCount, value); }
        }

        private int _totalInvoicesCount;
        public int TotalInvoicesCount
        {
            get { return _totalInvoicesCount; }
            set { SetProperty(ref _totalInvoicesCount, value); }
        }

        public CustomerOrdersViewModel()
        {
            IsLoading = true;
            LoadCustomerData();
        }

        public void LoadCustomerData()
        {
            IsLoading = true;

            try
            {
                using (var context = new ChinookContext())
                {
                    // Gets total counts
                    TotalCustomersCount = context.Customers.Count();
                    TotalInvoicesCount = context.Invoices.Count();

                    // Makes sure to eagerly load all related data
                    var query = context.Customers
                        .Include(c => c.Invoices)
                            .ThenInclude(i => i.InvoiceLines)
                                .ThenInclude(il => il.Track)
                        .AsQueryable();

                    // Apply search filter if provided
                    if (!string.IsNullOrWhiteSpace(SearchText))
                    {
                        string searchLower = SearchText.ToLower();
                        query = query.Where(c =>
                            c.FirstName.ToLower().Contains(searchLower) ||
                            c.LastName.ToLower().Contains(searchLower) ||
                            c.Email.ToLower().Contains(searchLower) ||
                            c.City.ToLower().Contains(searchLower) ||
                            c.Country.ToLower().Contains(searchLower) ||
                            (c.State != null && c.State.ToLower().Contains(searchLower))
                        );
                    }

                    var customerList = query.ToList();

                    // Transform to display model
                    var customerOrders = customerList.Select(c => new CustomerWithInvoices
                    {
                        CustomerId = c.CustomerId,
                        FullName = $"{c.LastName}, {c.FirstName}",
                        LocationInfo = !string.IsNullOrEmpty(c.State) ? $"{c.City}, {c.State}" : c.City,
                        Country = c.Country ?? "Unknown",
                        Email = c.Email ?? "No email",
                        Invoices = new ObservableCollection<InvoiceWithDetails>(
                            c.Invoices.Select(i => new InvoiceWithDetails
                            {
                                InvoiceId = i.InvoiceId,
                                InvoiceDate = i.InvoiceDate,
                                Total = i.Total,
                                TrackCount = i.InvoiceLines.Count,
                                InvoiceItems = new ObservableCollection<InvoiceItemDetail>(
                                    i.InvoiceLines.Select(il => new InvoiceItemDetail
                                    {
                                        TrackName = il.Track?.Name ?? "Unknown Track",
                                        UnitPrice = il.UnitPrice,
                                        TrackLength = il.Track != null ? FormatDuration(il.Track.Milliseconds) : "0:00"
                                    }).ToList()
                                )
                            }).OrderByDescending(i => i.InvoiceDate)
                        ),
                        TotalSpent = c.Invoices.Sum(i => i.Total),
                        FirstPurchaseDate = c.Invoices.Any() ? c.Invoices.Min(i => i.InvoiceDate) : DateTime.MinValue,
                        LastPurchaseDate = c.Invoices.Any() ? c.Invoices.Max(i => i.InvoiceDate) : DateTime.MinValue,
                        AverageOrderValue = c.Invoices.Any() ? c.Invoices.Average(i => i.Total) : 0
                    }).OrderBy(c => c.FullName).ToList();

                    // Update the observable collection on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Customers = new ObservableCollection<CustomerWithInvoices>(customerOrders);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer orders: {ex.Message}",
                                "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        // Helper method to format duration
        private string FormatDuration(int milliseconds)
        {
            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            if (time.Hours > 0)
                return $"{time.Hours}:{time.Minutes:D2}:{time.Seconds:D2}";
            else
                return $"{time.Minutes}:{time.Seconds:D2}";
        }
    }

    public class CustomerWithInvoices
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = "";
        public string LocationInfo { get; set; } = "";
        public string Country { get; set; } = "";
        public string Email { get; set; } = "";
        public ObservableCollection<InvoiceWithDetails> Invoices { get; set; } = new ObservableCollection<InvoiceWithDetails>();
        public decimal TotalSpent { get; set; }
        public DateTime FirstPurchaseDate { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public decimal AverageOrderValue { get; set; }
    }

    public class InvoiceWithDetails
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Total { get; set; }
        public int TrackCount { get; set; }
        public ObservableCollection<InvoiceItemDetail> InvoiceItems { get; set; } = new ObservableCollection<InvoiceItemDetail>();
    }

    public class InvoiceItemDetail
    {
        public string TrackName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public string TrackLength { get; set; } = "";
    }
}