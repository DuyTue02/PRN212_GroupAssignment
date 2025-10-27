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
    public partial class CustomerMenu : Window
    {
        public CustomerMenu(string fullName)
        {
            InitializeComponent();
            txtWelcome.Text = $"Welcome, {fullName}!";
        }

        private void btnRentalOptions_Click(object sender, RoutedEventArgs e)
        {
            RentMenu rentMenu = new RentMenu();
            rentMenu.Show();
            this.Close();
        }
    }
}
