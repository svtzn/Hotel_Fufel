using Hotel_Fufel.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Hotel_Fufel.ViewModels
{
    public class AdminUsersViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterUsers();
            }
        }

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private List<User> _allUsers = new List<User>();

        public AdminUsersViewModel()
        {
            _allUsers = _context.Users.ToList();
            FilterUsers();
        }

        private void FilterUsers()
        {
            Users.Clear();
            var filtered = _allUsers
                .Where(u => string.IsNullOrWhiteSpace(SearchText)
                         || u.Name.ToLower().Contains(SearchText.ToLower()))
                .ToList();

            foreach (var user in filtered)
                Users.Add(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
