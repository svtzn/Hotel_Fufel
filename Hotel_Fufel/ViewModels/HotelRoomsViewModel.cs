using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    public class HotelRoomsViewModel : INotifyPropertyChanged
    {
        // 1) Сама коллекция комнат
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();

        // 2) Выбранная комната
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(nameof(SelectedRoom)); RecalculateTotal(); }
        }

        // 3) Даты
        private DateTime? _checkInDate;
        public DateTime? CheckInDate
        {
            get => _checkInDate;
            set { _checkInDate = value; OnPropertyChanged(nameof(CheckInDate)); RecalculateTotal(); }
        }

        private DateTime? _checkOutDate;
        public DateTime? CheckOutDate
        {
            get => _checkOutDate;
            set { _checkOutDate = value; OnPropertyChanged(nameof(CheckOutDate)); RecalculateTotal(); }
        }

        // 4) Итого к оплате
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            private set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        // 5) Опции фильтров и выбранные значения
        public ObservableCollection<int> RoomsAmountOptions { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5 };
        private int _selectedRoomsAmount;
        public int SelectedRoomsAmount
        {
            get => _selectedRoomsAmount;
            set { _selectedRoomsAmount = value; OnPropertyChanged(nameof(SelectedRoomsAmount)); FilterRooms(); }
        }

        public ObservableCollection<string> BalconyOptions { get; } = new ObservableCollection<string> { "Все", "Да", "Нет" };
        private string _selectedBalconyOption;
        public string SelectedBalconyOption
        {
            get => _selectedBalconyOption;
            set { _selectedBalconyOption = value; OnPropertyChanged(nameof(SelectedBalconyOption)); FilterRooms(); }
        }

        public ObservableCollection<string> ComfortLevelOptions { get; } = new ObservableCollection<string> { "Все", "Эконом", "Стандарт", "Люкс" };
        private string _selectedComfortLevel;
        public string SelectedComfortLevel
        {
            get => _selectedComfortLevel;
            set { _selectedComfortLevel = value; OnPropertyChanged(nameof(SelectedComfortLevel)); FilterRooms(); }
        }

        public ObservableCollection<string> WiFiOptions { get; } = new ObservableCollection<string> { "Все", "Да", "Нет" };
        private string _selectedWiFiOption;
        public string SelectedWiFiOption
        {
            get => _selectedWiFiOption;
            set { _selectedWiFiOption = value; OnPropertyChanged(nameof(SelectedWiFiOption)); FilterRooms(); }
        }

        public ObservableCollection<string> KitchenOptions { get; } = new ObservableCollection<string> { "Все", "Да", "Нет" };
        private string _selectedKitchenOption;
        public string SelectedKitchenOption
        {
            get => _selectedKitchenOption;
            set { _selectedKitchenOption = value; OnPropertyChanged(nameof(SelectedKitchenOption)); FilterRooms(); }
        }

        public ObservableCollection<string> SmokeOptions { get; } = new ObservableCollection<string> { "Все", "Да", "Нет" };
        private string _selectedSmokeOption;
        public string SelectedSmokeOption
        {
            get => _selectedSmokeOption;
            set { _selectedSmokeOption = value; OnPropertyChanged(nameof(SelectedSmokeOption)); FilterRooms(); }
        }

        // 6) Команда бронирования
        public ICommand BookCommand { get; }

        public HotelRoomsViewModel()
        {
            // Заполнение тестовыми комнатами
            Rooms.Add(new Room { Id = 1, ComfortLevel = "Люкс", Description = "Wi‑Fi, Балкон", WiFi = true, Square = 15.00, RoomsAmount = 3, Shower = true, Smoke = true, Kitchen = true, Balcony = true, PricePerNight = 4500, ImagePath = "pack://application:,,,/Resources/room1.jpg" });
            Rooms.Add(new Room { Id = 2, ComfortLevel = "Стандарт", Description = "Кондиционер, Завтрак", PricePerNight = 2800, Balcony = false, Kitchen = false, Smoke = false, Shower = true, WiFi = true, RoomsAmount = 1, Square = 10, ImagePath = "Resources/room2.jpg" });

            Rooms.Add(new Room { Id = 3, ComfortLevel = "Стандарт", Description = "хуй", PricePerNight = 3000, Balcony = true, Smoke = false, Kitchen = false, RoomsAmount = 1, WiFi = false, Shower = true, Square = 12.00, ImagePath = "Resources/room2.jpg" });
            Rooms.Add(new Room { Id = 4, ComfortLevel = "Люкс", Description = "хуй", PricePerNight = 5000, Balcony = true, Smoke = false, Kitchen = true, RoomsAmount = 2, WiFi = true, Shower = true, Square = 18.00, ImagePath = "Resources/room2.jpg" });
            Rooms.Add(new Room { Id = 5, ComfortLevel = "Стандарт", Description = "хуй", PricePerNight = 3400, Balcony = true, Smoke = true, Kitchen = false, RoomsAmount = 4, WiFi = true, Shower = true, Square = 16.00, ImagePath = "Resources/room2.jpg" });
            // … добавьте остальные

            SelectedRoomsAmount = 1;
            SelectedBalconyOption = "Все";
            SelectedComfortLevel = "Все";
            SelectedWiFiOption = "Все";
            SelectedKitchenOption = "Все";
            SelectedSmokeOption = "Все";

            BookCommand = new RelayCommand<object>(_ =>
                MessageBox.Show($"Бронирование:\nКомната: {SelectedRoom?.ComfortLevel}\n" +
                                $"С {CheckInDate:dd.MM.yyyy} по {CheckOutDate:dd.MM.yyyy}\nИтого: {TotalPrice:F0}₽"),
                _ => SelectedRoom != null && CheckInDate.HasValue && CheckOutDate.HasValue && CheckOutDate > CheckInDate);
        }

        // Пересчёт итоговой суммы
        private void RecalculateTotal()
        {
            if (SelectedRoom != null && CheckInDate.HasValue && CheckOutDate.HasValue && CheckOutDate > CheckInDate)
            {
                var days = (CheckOutDate.Value - CheckInDate.Value).Days;
                TotalPrice = days * SelectedRoom.PricePerNight;
            }
            else
            {
                TotalPrice = 0;
            }
        }

        // Заглушка для фильтрации — в реале подставите логику по БД или LINQ
        private void FilterRooms()
        {
            // TODO: применить фильтры к коллекции Rooms (напр. через CollectionViewSource)
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
