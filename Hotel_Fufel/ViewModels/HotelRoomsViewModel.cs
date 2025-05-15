using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    // Простая реализация ICommand
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        public RelayCommand(Action<T> execute) { _execute = execute; }
        public bool CanExecute(object parameter) => true;
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter) => _execute((T)parameter);
    }

    // ViewModel для страницы HotelRooms
    public class HotelRoomsViewModel
    {
        // Коллекция комнат
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();

        // Команда бронирования
        public ICommand BookCommand { get; }

        public HotelRoomsViewModel()
        {
            // Заполнение тестовыми данными (заглушками)
            Rooms.Add(new Room { Id = 1, ComfortLevel = "Люкс", Description = "Wi‑Fi, Балкон", WiFi = true, Square = 15.00, RoomsAmount = 3, Shower = true, Smoke = true, Kitchen = true, Balcony = true, PricePerNight = 4500, ImagePath = "pack://application:,,,/Resources/room1.jpg" });
            Rooms.Add(new Room { Id = 2, ComfortLevel = "Стандарт", Description = "Кондиционер, Завтрак", PricePerNight = 2800, Balcony = false, Kitchen = false, Smoke = false, Shower = true, WiFi = true, RoomsAmount = 1, Square = 10, ImagePath = "Resources/room2.jpg" });

            Rooms.Add(new Room { Id =3, ComfortLevel = "Стандарт", Description = "хуй", PricePerNight = 3000, Balcony = true, Smoke = false, Kitchen = false, RoomsAmount = 1, WiFi = false, Shower = true, Square = 12.00, ImagePath = "Resources/room2.jpg" });
            Rooms.Add(new Room { Id =4, ComfortLevel = "Люкс", Description = "хуй", PricePerNight = 5000, Balcony = true, Smoke = false, Kitchen = true, RoomsAmount = 2, WiFi = true, Shower = true, Square = 18.00, ImagePath = "Resources/room2.jpg" });
            Rooms.Add(new Room { Id =5, ComfortLevel = "Стандарт", Description = "хуй", PricePerNight = 3400, Balcony = true, Smoke = true, Kitchen = false, RoomsAmount = 4, WiFi = true, Shower = true, Square = 16.00, ImagePath = "Resources/room2.jpg" });

            // Инициализируем команду: пока она просто показывает MessageBox
            BookCommand = new RelayCommand<int>(roomId =>
                MessageBox.Show($"Запрос на бронирование комнаты {roomId}"));
        }
    }
}
