using System.Windows;
using System.Windows.Controls;

namespace Hotel_Fufel
{
    public partial class AdminPage : Page
    {
        private readonly MainWindow _mainWindow;
        public AdminPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            ContentFrame.Navigate(new AdminWelcomePage());
        }

        private void Bookings_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AdminBookingsPage());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AdminUsersPage());
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new AdminRoomsPage());
        }
    }
}
