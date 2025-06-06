﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Hotel_Fufel.Services;

namespace Hotel_Fufel.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            private set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        public string RegName { get; set; }
        public string RegEmail { get; set; }
        public string RegPassword { get; set; }

        public string LoginEmail { get; set; }
        public string LoginPassword { get; set; }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public UserViewModel()
        {
            RegisterCommand = new RelayCommand<object>(_ =>
            {
                if (UserRepository.EmailExists(RegEmail))
                {
                    MessageBox.Show("Этот email уже занят.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = new User { Name = RegName, Email = RegEmail, Password = RegPassword };
                UserRepository.Add(user);

                MessageBox.Show("Регистрация успешна!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                CurrentUser = user;
            },
            _ => !string.IsNullOrWhiteSpace(RegName)
               && !string.IsNullOrWhiteSpace(RegEmail)
               && !string.IsNullOrWhiteSpace(RegPassword));

            LoginCommand = new RelayCommand<object>(_ =>
            {
                var user = UserRepository.Find(LoginEmail, LoginPassword);
                if (user == null)
                {
                    MessageBox.Show("Неверный email или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CurrentUser = user;
                MessageBox.Show($"Добро пожаловать, {user.Name}!", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            },
            _ => !string.IsNullOrWhiteSpace(LoginEmail)
               && !string.IsNullOrWhiteSpace(LoginPassword));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
