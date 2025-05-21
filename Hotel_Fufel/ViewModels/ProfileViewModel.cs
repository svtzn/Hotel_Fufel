using Hotel_Fufel.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    public class BookingDisplay
    {
        public int BookingId { get; set; }
        public Room Room { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice => (CheckOut - CheckIn).Days * Room.PricePerNight;
    }

    public class ProfileViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();
        private readonly User _user;

        public string Name => _user.Name;
        public string Email => _user.Email;
        public string ProfilePic => _user.ProfilePic;

        public ObservableCollection<BookingDisplay> Bookings { get; set; } = new ObservableCollection<BookingDisplay>();
        private BookingDisplay _selectedBooking;
        public BookingDisplay SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(nameof(SelectedBooking)); }
        }

        public ICommand CancelBookingCommand { get; }

        public ProfileViewModel(User user)
        {
            _user = user;
            LoadBookings();

            CancelBookingCommand = new RelayCommand<object>(_ =>
            {
                if (SelectedBooking == null) return;

                var booking = _context.Bookings.FirstOrDefault(b => b.Id == SelectedBooking.BookingId);
                if (booking != null)
                {
                    var room = _context.Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
                    if (room != null) room.IsFree = true;

                    _context.Bookings.Remove(booking);
                    _context.SaveChanges();

                    Bookings.Remove(SelectedBooking);
                    MessageBox.Show("Бронирование отменено");
                }
            }, _ => SelectedBooking != null);
        }

        private void LoadBookings()
        {
            var list = _context.Bookings
                .Where(b => b.UserId == _user.Id)
                .Select(b => new BookingDisplay
                {
                    BookingId = b.Id,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    Room = b.Room
                }).ToList();

            Bookings = new ObservableCollection<BookingDisplay>(list);
            OnPropertyChanged(nameof(Bookings));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
