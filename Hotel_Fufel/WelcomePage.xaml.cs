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
    }
}
