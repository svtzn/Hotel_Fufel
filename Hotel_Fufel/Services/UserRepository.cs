using System;
using System.Linq;
using Hotel_Fufel.Data;

namespace Hotel_Fufel.Services
{
    public static class UserRepository
    {
        public static bool EmailExists(string email)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.Any(u => u.Email.ToLower() == email.ToLower());
            }
        }

        public static void Add(User user)
        {
            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static User Find(string email, string password)
        {
            using (var db = new AppDbContext())
            {
                return db.Users.FirstOrDefault(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.Password == password);
            }
        }
    }
}
