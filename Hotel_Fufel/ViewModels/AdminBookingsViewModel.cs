using Hotel_Fufel.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


namespace Hotel_Fufel.ViewModels
{
    public class AdminBookingsViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ObservableCollection<Booking> Bookings { get; set; } = new ObservableCollection<Booking>();

        private Booking _selectedBooking;
        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(nameof(SelectedBooking)); }
        }

        public ICommand CancelBookingCommand { get; }

        public AdminBookingsViewModel()
        {
            LoadBookings();

            CancelBookingCommand = new RelayCommand<object>(
                _ =>
                {
                    if (SelectedBooking == null) return;

                    var booking = _context.Bookings.Find(SelectedBooking.Id);
                    if (booking != null)
                    {
                        var room = _context.Rooms.Find(booking.RoomId);
                        if (room != null) room.IsFree = true;

                        _context.Bookings.Remove(booking);
                        _context.SaveChanges();

                        Bookings.Remove(SelectedBooking);
                        MessageBox.Show("Бронирование отменено.");
                    }
                },
                _ => SelectedBooking != null
            );
        }

        private void LoadBookings()
        {
            var list = _context.Bookings.ToList();

            foreach (var booking in list)
            {
                booking.Room = _context.Rooms.Find(booking.RoomId);
                booking.User = _context.Users.Find(booking.UserId);
            }

            Bookings = new ObservableCollection<Booking>(list);
            OnPropertyChanged(nameof(Bookings));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
