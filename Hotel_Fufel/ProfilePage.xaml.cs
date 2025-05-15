using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel_Fufel
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>


    public partial class ProfilePage : Page
    {
        private readonly MainWindow _mainWindow;
        public ProfilePage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new ProfilePage(_mainWindow));
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new ProfilePage(_mainWindow));
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new WelcomePage(_mainWindow, WelcomePage.currentUser));
        }

        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new HotelRooms(_mainWindow));
        }
    }
}
