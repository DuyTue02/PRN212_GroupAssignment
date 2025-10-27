using BLL.Services;
using DAL.Entities;
using System.Windows;

namespace GroupAssignment
{
    public partial class LoginWindow : Window
    {
        private readonly UserAccountService _userAccountService;

        public LoginWindow()
        {
            InitializeComponent();
            _userAccountService = new UserAccountService();

            txtPassword.Password = "admin123";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            User userAccount = _userAccountService.GetUserAccount(email, password);

            if (userAccount == null)
            {
                MessageBox.Show("Email hoặc mật khẩu không đúng.", "Login Failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (userAccount.Role == "Admin" || userAccount.Role == "Staff")
            {
                MessageBox.Show($"Chào mừng {userAccount.FullName}! Vai trò: {userAccount.Role}");
                VehicleManagementWindow vehicleWindow = new VehicleManagementWindow(userAccount);
                vehicleWindow.Show();

                this.Close();
            }
            else if (userAccount.Role == "Customer")
            {
                MessageBox.Show($"Welcome {userAccount.FullName}! Role: {userAccount.Role}");
                CustomerMenu customerMenu = new CustomerMenu(userAccount.FullName);
                customerMenu.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này.", "Access Denied",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}