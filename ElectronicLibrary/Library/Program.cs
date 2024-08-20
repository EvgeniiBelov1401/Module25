using Microsoft.EntityFrameworkCore;
using Library.DAL;
using AppContext = Library.DAL.AppContext;
using Library.DAL.Entities;
using Library.DAL.Repositories;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {

                //    var user1 = new User { Name = "Александр Иванов", Email = "alivanov@gmail.com" };
                //    var user2 = new User { Name = "Федор Петров", Email = "fedpetrov@mail.ru" };
                //    var user3 = new User { Name = "Петр Смирнов", Email = "pesmirnov@yandex.ru" };
                //    db.Users.AddRange(user1, user2, user3);
                //    db.SaveChanges();

                //    var book1 = new Book { Title = "Преступление и наказание", YearOfRealise = "1866" };
                //    var book2 = new Book { Title = "Тихий дон", YearOfRealise = "1932" };
                //    var book3 = new Book { Title = "Граф Моонте-Кристо", YearOfRealise = "1846" };
                //    db.Books.AddRange(book1, book2, book3);
                //    db.SaveChanges();

                var user = new UserRepository();
                var book = new BookRepository();
                
                user.Add(db);
                user.Add(db);

                book.Add(db);
                book.Add(db);

                Console.ReadLine();
                //user.Drop(db);
                //book.Drop(db);
                user.ShowAll(db);
                book.ShowAll(db);
            }

            
        }
    }
}
