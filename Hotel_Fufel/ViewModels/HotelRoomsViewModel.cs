using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Hotel_Fufel.Data;

namespace Hotel_Fufel.ViewModels
{
    public class HotelRoomsViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();

        // Фильтры
        public List<int?> RoomsAmountOptions { get; } = new List<int?> { null, 1, 2, 3, 4 };
        public List<string> ComfortLevelOptions { get; } = new List<string> { "Все", "Эконом", "Стандарт", "Люкс" };
        public List<string> BalconyOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> KitchenOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> WiFiOptions { get; } = new List<string> { "Все", "Да", "Нет" };
        public List<string> SmokeOptions { get; } = new List<string> { "Все", "Да", "Нет" };

        private int? _selectedRoomsAmount;
        private string _selectedComfortLevel = "Все";
        private string _selectedBalconyOption = "Все";
        private string _selectedKitchenOption = "Все";
        private string _selectedWiFiOption = "Все";
        private string _selectedSmokeOption = "Все";

        public int? SelectedRoomsAmount
        {
            get => _selectedRoomsAmount;
            set { _selectedRoomsAmount = value; OnPropertyChanged(nameof(SelectedRoomsAmount)); FilterRooms(); }
        }

        public string SelectedComfortLevel
        {
            get => _selectedComfortLevel;
            set { _selectedComfortLevel = value; OnPropertyChanged(nameof(SelectedComfortLevel)); FilterRooms(); }
        }

        public string SelectedBalconyOption
        {
            get => _selectedBalconyOption;
            set { _selectedBalconyOption = value; OnPropertyChanged(nameof(SelectedBalconyOption)); FilterRooms(); }
        }

        public string SelectedKitchenOption
        {
            get => _selectedKitchenOption;
            set { _selectedKitchenOption = value; OnPropertyChanged(nameof(SelectedKitchenOption)); FilterRooms(); }
        }

        public string SelectedWiFiOption
        {
            get => _selectedWiFiOption;
            set { _selectedWiFiOption = value; OnPropertyChanged(nameof(SelectedWiFiOption)); FilterRooms(); }
        }

        public string SelectedSmokeOption
        {
            get => _selectedSmokeOption;
            set { _selectedSmokeOption = value; OnPropertyChanged(nameof(SelectedSmokeOption)); FilterRooms(); }
        }

        // Основные свойства
        public User CurrentUser { get; set; }
        public ObservableCollection<Room> Rooms { get; } = new ObservableCollection<Room>();

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(nameof(SelectedRoom)); RecalculateTotal(); }
        }

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

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            private set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
        }

        public ICommand BookCommand { get; }

        public HotelRoomsViewModel()
        {
            LoadRoomsFromDb();

            BookCommand = new RelayCommand<object>(_ =>
            {
                if (CurrentUser == null)
                {
                    MessageBox.Show("Ошибка: пользователь не найден.");
                    return;
                }

                var booking = new Booking
                {
                    UserId = CurrentUser.Id,
                    RoomId = SelectedRoom.Id,
                    CheckInDate = CheckInDate.Value,
                    CheckOutDate = CheckOutDate.Value
                };

                _context.Bookings.Add(booking);
                SelectedRoom.IsFree = false;
                _context.SaveChanges();

                MessageBox.Show("Бронирование успешно сохранено!");
                LoadRoomsFromDb();
            },
            _ => SelectedRoom != null && CheckInDate.HasValue && CheckOutDate.HasValue && CheckOutDate > CheckInDate);
        }

        private void LoadRoomsFromDb()
        {
            FilterRooms();
        }

        private void FilterRooms()
        {
            Rooms.Clear();

            var query = _context.Rooms.Where(r => r.IsFree);

            if (SelectedRoomsAmount.HasValue)
                query = query.Where(r => r.RoomsAmount == SelectedRoomsAmount.Value);

            if (SelectedComfortLevel != "Все")
                query = query.Where(r => r.ComfortLevel == SelectedComfortLevel);

            query = ApplyFilter(query, SelectedBalconyOption, r => r.Balcony);
            query = ApplyFilter(query, SelectedKitchenOption, r => r.Kitchen);
            query = ApplyFilter(query, SelectedWiFiOption, r => r.WiFi);
            query = ApplyFilter(query, SelectedSmokeOption, r => r.Smoke);

            foreach (var room in query.ToList())
                Rooms.Add(room);

            RecalculateTotal();
        }

        // Исправленный метод с использованием классического switch
        private IQueryable<Room> ApplyFilter(IQueryable<Room> query, string filter, Func<Room, bool> predicate)
        {
            switch (filter)
            {
                case "Да":
                    return query.Where(predicate).AsQueryable();
                case "Нет":
                    return query.Where(r => !predicate(r)).AsQueryable();
                default:
                    return query;
            }
        }

        private void RecalculateTotal()
        {
            if (SelectedRoom != null && CheckInDate.HasValue && CheckOutDate.HasValue && CheckOutDate > CheckInDate)
            {
                int days = (CheckOutDate.Value - CheckInDate.Value).Days;
                TotalPrice = days * SelectedRoom.PricePerNight;
            }
            else
            {
                TotalPrice = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}