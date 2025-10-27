using DAL.Entities;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for RentMenu.xaml
    /// </summary>
    public partial class RentMenu : Window
    {
        private readonly EVRentalDBContext _dbContext;

        public RentMenu()
        {
            InitializeComponent();
            _dbContext = new EVRentalDBContext();
            InitializeWebView2();
            LoadStations();
        }

        private async void InitializeWebView2()
        {
            try
            {
                await wbMap.EnsureCoreWebView2Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize WebView2: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadStations()
        {
            var stations = _dbContext.Stations.Select(s => new { s.StationId, s.StationName }).ToList();
            cbStations.ItemsSource = stations;
            cbStations.DisplayMemberPath = "StationName";
            cbStations.SelectedValuePath = "StationId";
        }

        private void btnFindOnMap_Click(object sender, RoutedEventArgs e)
        {
            if (cbStations.SelectedItem is null)
            {
                MessageBox.Show("Please select a station.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedStationId = (int)cbStations.SelectedValue;
            var station = _dbContext.Stations.FirstOrDefault(s => s.StationId == selectedStationId);

            if (station != null)
            {
                string address = station.Address;
                string mapUrl = $"https://www.google.com/maps?q={Uri.EscapeDataString(address)}";
                wbMap.CoreWebView2.Navigate(mapUrl);
            }
        }
    }
}

