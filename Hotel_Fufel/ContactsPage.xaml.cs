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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel_Fufel
{
    /// <summary>
    /// Логика взаимодействия для ContactsPage.xaml
    /// </summary>
    public partial class ContactsPage : Page
    {
        private readonly MainWindow _mainWindow;
        public ContactsPage(MainWindow mainWindow)
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
