using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hotel_Fufel
{
    public partial class WelcomePage : Page
    {
        private readonly MainWindow _mainWindow;
        public static User currentUser;

        public WelcomePage(MainWindow mainWindow, User user)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            currentUser = user;
            WelcomeText.Text = $"Добро пожаловать, {currentUser.Name}!";

        }
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new ProfilePage(_mainWindow));
        }

        private void Contacts_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new ContactsPage(_mainWindow));
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
