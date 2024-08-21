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
            var user=new User();
            var searchBook = new Book();
            Console.Write("Введите имя пользователя: ");
            var userName = Console.ReadLine();
            Console.Write("Введите Email пользователя: ");
            var userEmail = Console.ReadLine();
            #region Добавление Книги из таблицы Books
            Console.Write("Введите ID книги пользователя: ");
            int userBookId;
            try
            {
                if (int.TryParse(Console.ReadLine(), out userBookId))
                {
                    searchBook = db.Books.Where(b => b.Id == userBookId).FirstOrDefault();
                    try
                    {
                        if(searchBook == null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            user = new User { Name = userName, Email = userEmail, BookId = searchBook.Id };
                            db.Users.Add(user);
                            db.SaveChanges();
                            Console.WriteLine("Добавлен новый пользователь...");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Книги с таким ID нет в базе данных...\n");
                    }                                     
                }
                else
                {
                    throw new NoBookExistException();
                }
            }
            catch (NoBookExistException) 
            {
                Console.WriteLine("Книги с таким ID нет в базе данных...\n");
            }
            #endregion
        }

        public void Drop(AppContext db)
        {
            Console.Write("Для удаления введите имя пользователя: ");
            var userName = Console.ReadLine();
            var user = new User();
            user = db.Users.Where(u => u.Name == userName).FirstOrDefault();
            try
            {
                if (user==null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    db.Users.Remove(user);
            db.SaveChanges();
            Console.WriteLine("Пользователь удален...");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Пользователя с таким именем не существует...\n");
            }            
        }

        public void ShowAll(AppContext db)
        {
            var user = new User();
            var allUsers=db.Users.ToList();
            try
            {
                if (allUsers.Count==0)
                {
                    throw new EmptyTableException();
                }
                else
                {
                    Console.WriteLine($"{nameof(user.Id)}\t\t{nameof(user.Name)}\t\t{nameof(user.Email)}\t\t{nameof(user.BookId)}");
                    foreach (var us in allUsers)
                    {
                        Console.WriteLine($"{us.Id}\t\t{us.Name}\t\t{us.Email}\t\t{us.BookId}");
                    }
                    Console.WriteLine();
                }
            }
            catch(EmptyTableException)
            {
                Console.WriteLine("Таблица пустая...");
            }            
        }

        public void ShowById(AppContext db)
        {
            Console.Write("Введите ID клиента: ");
            try
            {
                int inputId;
                var userById = new User();
                if (int.TryParse(Console.ReadLine(),out inputId))
                {
                    userById = db.Users.Where(u => u.Id == inputId).FirstOrDefault();
                    try
                    {
                        if (userById==null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            Console.WriteLine($"{nameof(userById.Id)}\t\t{nameof(userById.Name)}\t\t{nameof(userById.Email)}\t\t{nameof(userById.BookId)}");
                            Console.WriteLine($"{userById.Id}\t\t{userById.Name}\t\t{userById.Email}\t\t{userById.BookId}");
                            Console.WriteLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Пользователя с таким ID не существует...\n");
                    }                                                           
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch(NoIdException)
            {
                Console.WriteLine("Пользователя с таким ID не существует...\n");
            }            
        }

        public void Update(AppContext db)
        {
            Console.Write("Введите ID клиента: ");
            try
            {
                var user = new User();
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {                   
                    user=db.Users.Where(u=>u.Id == inputId).FirstOrDefault();
                    try
                    {
                        if (user==null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            Console.Write("Введите новое имя пользователя: ");
                            var newUserName = Console.ReadLine();
                            user.Name = newUserName;
                            db.SaveChanges();
                            Console.WriteLine("Имя изменено...");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Пользователя с таким ID не существует...\n");
                    }                                        
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch (NoIdException)
            {
                Console.WriteLine("Пользователя с таким ID не существует...\n");
            }           
        }
    }
}
