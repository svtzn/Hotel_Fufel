using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel_Fufel.ViewModels;

namespace Hotel_Fufel
{
    /// <summary>
    /// Логика взаимодействия для HotelRooms.xaml
    /// </summary>
    public partial class HotelRooms : Page
    {
        private readonly MainWindow _mainWindow;
        private HotelRoomsViewModel VM => DataContext as HotelRoomsViewModel;
        public HotelRooms(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            var vm = new HotelRoomsViewModel();
            vm.CurrentUser = WelcomePage.currentUser;
            this.DataContext = vm;

        }
        private void Card_Click(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            var room = border.DataContext as Room;
            VM.SelectedRoom = room;
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
