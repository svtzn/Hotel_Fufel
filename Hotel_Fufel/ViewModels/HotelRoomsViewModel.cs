using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    public class HotelRoomsViewModel : INotifyPropertyChanged
    {
        // Все комнаты (заглушки)
        private readonly List<Room> _allRooms = new List<Room>();

        // Отфильтрованные комнаты
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();

        // Выбранная комната
        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(nameof(SelectedRoom)); RecalculateTotal(); }
        }

        // Даты
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

        // Итоговая цена
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            private set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        // Опции фильтрации
        public List<int?> RoomsAmountOptions { get; } = new List<int?> { null, 1, 2, 3, 4 };
        public List<string> ComfortLevelOptions { get; } = new List<string> { "Все", "Эконом", "Стандарт", "Люкс" };
        public List<string> BalconyOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> KitchenOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> WiFiOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> SmokeOptions { get; } = new List<string> { "Все", "Да", "Нет" };

        // Выбранные значения фильтров
        private int? _selectedRoomsAmount;
        public int? SelectedRoomsAmount
        {
            get => _selectedRoomsAmount;
            set { _selectedRoomsAmount = value; OnPropertyChanged(nameof(SelectedRoomsAmount)); FilterRooms(); }
        }

        private string _selectedComfortLevel = "Все";
        public string SelectedComfortLevel
        {
            get => _selectedComfortLevel;
            set { _selectedComfortLevel = value; OnPropertyChanged(nameof(SelectedComfortLevel)); FilterRooms(); }
        }

        private string _selectedBalconyOption = "Все";
        public string SelectedBalconyOption
        {
            get => _selectedBalconyOption;
            set { _selectedBalconyOption = value; OnPropertyChanged(nameof(SelectedBalconyOption)); FilterRooms(); }
        }

        private string _selectedKitchenOption = "Все";
        public string SelectedKitchenOption
        {
            get => _selectedKitchenOption;
            set { _selectedKitchenOption = value; OnPropertyChanged(nameof(SelectedKitchenOption)); FilterRooms(); }
        }

        private string _selectedWiFiOption = "Все";
        public string SelectedWiFiOption
        {
            get => _selectedWiFiOption;
            set { _selectedWiFiOption = value; OnPropertyChanged(nameof(SelectedWiFiOption)); FilterRooms(); }
        }

        private string _selectedSmokeOption = "Все";
        public string SelectedSmokeOption
        {
            get => _selectedSmokeOption;
            set { _selectedSmokeOption = value; OnPropertyChanged(nameof(SelectedSmokeOption)); FilterRooms(); }
        }

        // Команда бронирования
        public ICommand BookCommand { get; }

        public HotelRoomsViewModel()
        {
            // Заглушки
            _allRooms.Add(new Room { Id = 1, ComfortLevel = "Люкс", RoomsAmount = 2, Balcony = true, Kitchen = true, WiFi = true, Smoke = false, PricePerNight = 5500, IsFree = true, ImagePath = "pack://application:,,,/Resources/room1.jpg" });
            _allRooms.Add(new Room { Id = 2, ComfortLevel = "Стандарт", RoomsAmount = 1, Balcony = false, Kitchen = false, WiFi = true, Smoke = false, PricePerNight = 3200, IsFree = true, ImagePath = "pack://application:,,,/Resources/room2.jpg" });
            _allRooms.Add(new Room { Id = 3, ComfortLevel = "Эконом", RoomsAmount = 1, Balcony = false, Kitchen = false, WiFi = false, Smoke = false, PricePerNight = 2100, IsFree = true, ImagePath = "pack://application:,,,/Resources/room1.jpg" });
            _allRooms.Add(new Room { Id = 4, ComfortLevel = "Люкс", PricePerNight = 5000, Balcony = true, Smoke = false, Kitchen = true, RoomsAmount = 2, WiFi = true, Shower = true, IsFree = false, ImagePath = "Resources/room2.jpg"});
            _allRooms.Add(new Room { Id = 5, ComfortLevel = "Стандарт", PricePerNight = 3400, Balcony = true, Smoke = true, Kitchen = false, RoomsAmount = 4, WiFi = true, Shower = true, IsFree = false, ImagePath = "Resources/room2.jpg" });
            _allRooms.Add(new Room { Id = 6, ComfortLevel = "Стандарт", PricePerNight = 3400, Balcony = true, Smoke = true, Kitchen = false, RoomsAmount = 4, WiFi = true, Shower = true, IsFree = true, ImagePath = "Resources/room2.jpg" });
            _allRooms.Add(new Room { Id = 7, ComfortLevel = "Стандарт", PricePerNight = 3400, Balcony = true, Smoke = true, Kitchen = false, RoomsAmount = 4, WiFi = true, Shower = true, IsFree = true, ImagePath = "Resources/room2.jpg" });

            foreach (var r in _allRooms)
                Rooms.Add(r);

            // Команда
            BookCommand = new RelayCommand<object>(_ =>
                MessageBox.Show(
                    $"Бронирование:\nКомната: {SelectedRoom?.ComfortLevel}\n" +
                    $"С {CheckInDate:dd.MM.yyyy} по {CheckOutDate:dd.MM.yyyy}\n" +
                    $"Итого: {TotalPrice:F0} ₽"),
                _ => SelectedRoom != null && CheckInDate.HasValue && CheckOutDate.HasValue && CheckOutDate > CheckInDate);

            // Инициализация фильтров
            SelectedRoomsAmount = null;
            SelectedComfortLevel = "Все";
            SelectedBalconyOption = "Все";
            SelectedKitchenOption = "Все";
            SelectedWiFiOption = "Все";
            SelectedSmokeOption = "Все";
        }

        private void FilterRooms()
        {
            Rooms.Clear();
            var q = _allRooms.Where(r => r.IsFree &&
                (!SelectedRoomsAmount.HasValue || r.RoomsAmount == SelectedRoomsAmount.Value)
             && (SelectedComfortLevel == "Все" || r.ComfortLevel == SelectedComfortLevel)
             && (SelectedBalconyOption == "Все" || (SelectedBalconyOption == "Да" && r.Balcony) || (SelectedBalconyOption == "Нет" && !r.Balcony))
             && (SelectedKitchenOption == "Все" || (SelectedKitchenOption == "Да" && r.Kitchen) || (SelectedKitchenOption == "Нет" && !r.Kitchen))
             && (SelectedWiFiOption == "Все" || (SelectedWiFiOption == "Да" && r.WiFi) || (SelectedWiFiOption == "Нет" && !r.WiFi))
             && (SelectedSmokeOption == "Все" || (SelectedSmokeOption == "Да" && r.Smoke) || (SelectedSmokeOption == "Нет" && !r.Smoke))
            );

            foreach (var room in q)
                Rooms.Add(room);

            RecalculateTotal();
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
