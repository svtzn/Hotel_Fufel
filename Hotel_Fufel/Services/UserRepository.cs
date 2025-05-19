using System;
using System.Collections.Generic;
using System.Linq;
using Hotel_Fufel;

namespace Hotel_Fufel.Services
{
    public static class UserRepository
    {
        // Список всех зарегистрированных пользователей
        public static List<User> Users { get; } = new List<User>()
        {
            // Несколько «готовых» пользователей‑заглушек:
            new User { Name = "Иван Иванов", Email = "1", Password = "1" },
            new User { Name = "Егор", Email = "belobrysov06@bk.ru", Password = "123" }
        };

        public static bool EmailExists(string email) =>
            Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public static void Add(User user)
        {
            Users.Add(user);
            // для отладки: 
            System.Diagnostics.Debug.WriteLine($"UserRepository: added {user.Email}; total count = {Users.Count}");
        }

        public static User Find(string email, string password)
        {
            var u = Users.FirstOrDefault(x =>
                x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)
             && x.Password == password);

            System.Diagnostics.Debug.WriteLine(u == null
                ? $"UserRepository: Find({email},…) — NOT found"
                : $"UserRepository: Find({email},…) — found {u.Name}");

            return u;
        }
    }
}
