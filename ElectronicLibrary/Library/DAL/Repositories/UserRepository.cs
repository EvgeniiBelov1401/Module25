using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class UserRepository:IRepository
    {
        public void Add(AppContext db)
        {
            Console.Write("Введите имя пользователя: ");
            var userName = Console.ReadLine();
            Console.Write("Введите Email пользователя: ");
            var userEmail = Console.ReadLine();


            var user = new User { Name = userName, Email = userEmail };
            db.Users.Add(user);
            db.SaveChanges();
        }
       
        public void Drop(AppContext db)
        {
            Console.Write("Для удаления введите имя пользователя: ");
            var userName = Console.ReadLine();
            var user = db.Users.Where(u => u.Name == userName).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void ShowAll(AppContext db)
        {
            var user = new User();
            var allUsers=db.Users.ToList();

            Console.WriteLine($"{nameof(user.Id)}\t\t{nameof(user.Name)}\t\t{nameof(user.Email)}");
            foreach (var us in allUsers)
            {
                Console.WriteLine($"{us.Id}\t\t{us.Name}\t\t{us.Email}");
            }
            Console.WriteLine();
        }
    }
}
