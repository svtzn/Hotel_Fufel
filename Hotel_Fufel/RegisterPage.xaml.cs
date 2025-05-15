using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        // Обработчик кнопки "Зарегистрироваться"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirm = ConfirmBox.Password;
            bool isValid = true;

            var inputs = new (string Value, TextBlock ErrorText)[]
            {
                (NameTextBox.Text, NameError),
                (EmailTextBox.Text, EmailError),
            };


            foreach (var (value, error) in inputs)
            {
                bool empty = string.IsNullOrWhiteSpace(value);
                error.Text = empty ? "Поле не может быть пустым" : "";
                error.Visibility = empty ? Visibility.Visible : Visibility.Collapsed;
                if (empty) isValid = false;
            }

            bool passwordsEmpty = string.IsNullOrWhiteSpace(PasswordBox.Password);
            bool confirmEmpty = string.IsNullOrWhiteSpace(ConfirmBox.Password);

            PasswordError.Text = passwordsEmpty ? "Поле не может быть пустым" : "";
            PasswordError.Visibility = passwordsEmpty ? Visibility.Visible : Visibility.Collapsed;

            ConfirmPasswordError.Text = confirmEmpty ? "Поле не может быть пустым" :
                (PasswordBox.Password != ConfirmBox.Password ? "Пароли не совпадают" : "");
            ConfirmPasswordError.Visibility = (confirmEmpty || PasswordBox.Password != ConfirmBox.Password) ?
                Visibility.Visible : Visibility.Collapsed;

            if (passwordsEmpty || confirmEmpty || PasswordBox.Password != ConfirmBox.Password)
                isValid = false;

            if (isValid)
            {
                User newUser = new User
                {
                    Name = name,
                    Email = password,
                    Password = email,
                };

                _mainWindow.NavigateTo(new WelcomePage(_mainWindow, newUser));
            }
        }

        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateTo(new LoginPage(_mainWindow));
        }
    }
}
