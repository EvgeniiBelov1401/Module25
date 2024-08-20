using Library.DAL.Entities;
using Library.Exceptions;
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

        public void ShowById(AppContext db)
        {
            Console.Write("Введите ID клиента: ");
            try
            {
                int inputId;
                if(int.TryParse(Console.ReadLine(),out inputId))
                {
                    var user = new User();
                    int maxId = 0;
                    var allUsers = db.Users.ToList();
                    foreach(var us in allUsers)
                    {
                        if (us.Id > maxId) maxId = us.Id;
                    }
                    if (inputId <= 0 || inputId>maxId)
                    {
                        throw new NoIdException();
                    }
                    else
                    {
                        var userById = db.Users.Where(u => u.Id == inputId).ToList();
                        Console.WriteLine($"{nameof(user.Id)}\t\t{nameof(user.Name)}\t\t{nameof(user.Email)}");
                        foreach (var us in userById)
                        {
                            Console.WriteLine($"{us.Id}\t\t{us.Name}\t\t{us.Email}");
                        }
                        Console.WriteLine();
                    }                   
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch(NoIdException)
            {
                Console.WriteLine("Введите корректный ID\n");
            }
        }

        public void Update(AppContext db)
        {
            Console.Write("Введите ID клиента: ");
            try
            {
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {
                    int maxId = 0;
                    var allUsers = db.Users.ToList();
                    foreach (var us in allUsers)
                    {
                        if (us.Id > maxId) maxId = us.Id;
                    }
                    if (inputId <= 0 || inputId > maxId)
                    {
                        throw new NoIdException();
                    }
                    else
                    {
                        var user=db.Users.Where(u=>u.Id == inputId).FirstOrDefault();
                        Console.Write("Введите новое имя пользователя: ");
                        var newUserName=Console.ReadLine();
                        user.Name=newUserName;
                        db.SaveChanges();
                        Console.WriteLine("Имя изменено!!!");
                    }
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch (NoIdException)
            {
                Console.WriteLine("Введите корректный ID\n");
            }
        }
    }
}
