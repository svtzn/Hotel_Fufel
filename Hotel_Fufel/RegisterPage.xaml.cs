using System.Windows;
using System.Windows.Controls;
using Hotel_Fufel.Services;

namespace Hotel_Fufel
{
    public partial class RegisterPage : Page
    {
        private readonly MainWindow _mainWindow;
        public RegisterPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Читаем поля
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirm = ConfirmBox.Password;

            // Сбрасываем ошибки
            NameError.Text = EmailError.Text = PasswordError.Text = ConfirmPasswordError.Text = "";
            NameError.Visibility = EmailError.Visibility = PasswordError.Visibility = ConfirmPasswordError.Visibility = Visibility.Collapsed;

            // Проверки пустых
            if (string.IsNullOrWhiteSpace(name))
            {
                NameError.Text = "Поле не может быть пустым";
                NameError.Visibility = Visibility.Visible;
            }
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
            if (password != confirm)
            {
                ConfirmPasswordError.Text = string.IsNullOrWhiteSpace(confirm)
                    ? "Поле не может быть пустым"
                    : "Пароли не совпадают";
                ConfirmPasswordError.Visibility = Visibility.Visible;
            }

            // Если есть ошибки — выходим
            if (NameError.Visibility == Visibility.Visible ||
                EmailError.Visibility == Visibility.Visible ||
                PasswordError.Visibility == Visibility.Visible ||
                ConfirmPasswordError.Visibility == Visibility.Visible)
            {
                return;
            }

            // Проверяем уникальность e‑mail
            if (UserRepository.EmailExists(email))
            {
                MessageBox.Show("Пользователь с таким e‑mail уже существует.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаём нового пользователя и добавляем в репозиторий
            var newUser = new User
            {
                Name = name,
                Email = email,
                Password = password
            };
            UserRepository.Add(newUser);

            MessageBox.Show("Регистрация прошла успешно.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

            // Навигируем дальше
            _mainWindow.NavigateTo(new WelcomePage(_mainWindow, newUser));
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new LoginPage(_mainWindow));
        }
    }
}
