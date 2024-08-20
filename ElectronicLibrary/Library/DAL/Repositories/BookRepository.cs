using Library.DAL.Entities;
using Library.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class BookRepository:IRepository
    {
        public void Add(AppContext db)
        {
            Console.Write("Введите название книги: ");
            var bookTitle = Console.ReadLine();
            Console.Write("Введите год написания книги: ");
            var bookYearOfRealise = Console.ReadLine();


                var book = new Book { Title = bookTitle, YearOfRealise=bookYearOfRealise };
                db.Books.Add(book);
                db.SaveChanges();
            Console.WriteLine("Добавлена новая книга...");
        }

        public void Drop(AppContext db)
        {
            Console.Write("Для удаления введите название книги: ");
            var bookTitle = Console.ReadLine();
            var book = db.Books.Where(b => b.Title == bookTitle).FirstOrDefault();
            db.Books.Remove(book);
            db.SaveChanges();
            Console.WriteLine("Книга удалена...");
        }

        public void ShowAll(AppContext db)
        {
            var book = new Book();
            var allBooks = db.Books.ToList();

            Console.WriteLine($"{nameof(book.Id)}\t\t{nameof(book.Title)}\t\t{nameof(book.YearOfRealise)}");
            foreach (var bk in allBooks)
            {
                Console.WriteLine($"{bk.Id}\t\t{bk.Title}\t\t{bk.YearOfRealise}");
            }
            Console.WriteLine();
        }

        public void ShowById(AppContext db)
        {
            Console.Write("Введите ID книги: ");
            try
            {
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {
                    var book = new Book();
                    int maxId = 0;
                    var allBooks = db.Books.ToList();
                    foreach (var bk in allBooks)
                    {
                        if (bk.Id > maxId) maxId = bk.Id;
                    }
                    if (inputId <= 0 || inputId > maxId)
                    {
                        throw new NoIdException();
                    }
                    else
                    {
                        var bookById = db.Books.Where(b => b.Id == inputId).ToList();
                        Console.WriteLine($"{nameof(book.Id)}\t\t{nameof(book.Title)}\t\t{nameof(book.YearOfRealise)}");
                        foreach (var bk in bookById)
                        {
                            Console.WriteLine($"{bk.Id}\t\t{bk.Title}\t\t{bk.YearOfRealise}");
                        }
                        Console.WriteLine();
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

        public void Update(AppContext db)
        {
            Console.Write("Введите ID книги: ");
            try
            {
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {
                    int maxId = 0;
                    var allBooks = db.Books.ToList();
                    foreach (var bk in allBooks)
                    {
                        if (bk.Id > maxId) maxId = bk.Id;
                    }
                    if (inputId <= 0 || inputId > maxId)
                    {
                        throw new NoIdException();
                    }
                    else
                    {
                        var book = db.Books.Where(b => b.Id == inputId).FirstOrDefault();
                        Console.Write("Введите новый год выпуска книги: ");
                        var newYearOfRealiseBook = Console.ReadLine();
                        book.YearOfRealise = newYearOfRealiseBook;
                        db.SaveChanges();
                        Console.WriteLine("Год выпуска изменен...");
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
