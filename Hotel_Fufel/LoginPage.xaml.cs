using System.Windows;
using System.Windows.Controls;
using Hotel_Fufel.Services;

namespace Hotel_Fufel
{
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
            // Читаем поля
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            // Сбрасываем ошибки
            EmailError.Text = PasswordError.Text = "";
            EmailError.Visibility = PasswordError.Visibility = Visibility.Collapsed;

            // Проверки пустых
            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError.Text = "Поле не может быть пустым";
                EmailError.Visibility = Visibility.Visible;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordError.Text = "Поле не может быть пустым";
                PasswordError.Visibility = Visibility.Visible;
            }
            if (EmailError.Visibility == Visibility.Visible ||
                PasswordError.Visibility == Visibility.Visible)
            {
                return;
            }

            if (email == "admin" && password == "admin")
            {
                // Переход на админ-панель
                _mainWindow.NavigateTo(new AdminPage(_mainWindow));
                return;
            }

            // Ищем пользователя в репозитории
            var user = UserRepository.Find(email, password);
            if (user == null)
            {
                MessageBox.Show("Неверный e‑mail или пароль.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Успешный вход
            _mainWindow.NavigateTo(new WelcomePage(_mainWindow, user));
        }
    }
}
