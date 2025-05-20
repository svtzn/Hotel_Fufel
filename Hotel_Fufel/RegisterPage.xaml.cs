using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_Fufel.Data; // Обязательно!
using Hotel_Fufel.ViewModels; // Для WelcomePage

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
            string name = NameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirm = ConfirmBox.Password;

            NameError.Text = EmailError.Text = PasswordError.Text = ConfirmPasswordError.Text = "";
            NameError.Visibility = EmailError.Visibility = PasswordError.Visibility = ConfirmPasswordError.Visibility = Visibility.Collapsed;

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

            if (NameError.Visibility == Visibility.Visible ||
                EmailError.Visibility == Visibility.Visible ||
                PasswordError.Visibility == Visibility.Visible ||
                ConfirmPasswordError.Visibility == Visibility.Visible)
            {
                return;
            }

            using (var db = new AppDbContext())
            {
                // Проверка на уникальность email
                if (db.Users.Any(u => u.Email == email))
                {
                    MessageBox.Show("Пользователь с таким e‑mail уже существует.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);

                _mainWindow.NavigateTo(new WelcomePage(_mainWindow, newUser));
            }
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new LoginPage(_mainWindow));
        }
    }
}
