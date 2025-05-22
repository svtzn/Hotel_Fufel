using Hotel_Fufel.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    public class AdminRoomsViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ObservableCollection<Room> Rooms { get; set; }
        public Room SelectedRoom { get; set; }

        public ICommand AddRoomCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public AdminRoomsViewModel()
        {
            LoadRooms();

            AddRoomCommand = new RelayCommand<object>(_ =>
            {
                var newRoom = new Room
                {
                    ComfortLevel = "Стандарт",
                    RoomsAmount = 1,
                    PricePerNight = 1000,
                    Balcony = false,
                    Kitchen = false,
                    WiFi = false,
                    Smoke = false,
                    Shower = true,
                    IsFree = true
                };
                _context.Rooms.Add(newRoom);
                _context.SaveChanges();
                Rooms.Add(newRoom);
            });

            SaveChangesCommand = new RelayCommand<object>(_ =>
            {
                _context.SaveChanges();
                MessageBox.Show("Изменения сохранены", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        private void LoadRooms()
        {
            Rooms = new ObservableCollection<Room>(_context.Rooms.ToList());
            OnPropertyChanged(nameof(Rooms));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
