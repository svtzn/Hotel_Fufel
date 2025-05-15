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
using System.Xml.Linq;

namespace Hotel_Fufel
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly MainWindow _mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new RegisterPage(_mainWindow));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            bool isValid = true;

            var inputs = new (string Value, TextBlock ErrorText)[]
            {
                (PasswordBox.Password, PasswordError),
                (EmailTextBox.Text, EmailError),
            };


            foreach (var (value, error) in inputs)
            {
                bool empty = string.IsNullOrWhiteSpace(value);
                error.Text = empty ? "Поле не может быть пустым" : "";
                error.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
                if (empty) isValid = false;
            }

            if (isValid)
            {
                User newUser = new User
                {
                    Name = email,
                    Password = password,
                    Email = email
                };
                _mainWindow.NavigateTo(new WelcomePage(_mainWindow, newUser));
            }
        }
    }
}
