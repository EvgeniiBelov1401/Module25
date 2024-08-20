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
            var book = new Book();
            book = db.Books.Where(b => b.Title == bookTitle).FirstOrDefault();
            try
            {
                if (book == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    Console.WriteLine("Книга удалена...");
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Книги с таким названием нет в базе данных...\n");
            }            
        }

        public void ShowAll(AppContext db)
        {
            var book = new Book();
            var allBooks = db.Books.ToList();
            try
            {
                if (allBooks.Count==0)
                {
                    throw new EmptyTableException();
                }
                else
                {
                    Console.WriteLine($"{nameof(book.Id)}\t\t{nameof(book.Title)}\t\t{nameof(book.YearOfRealise)}");
                    foreach (var bk in allBooks)
                    {
                        Console.WriteLine($"{bk.Id}\t\t{bk.Title}\t\t{bk.YearOfRealise}");
                    }
                    Console.WriteLine();
                }
            }catch (EmptyTableException)
            {
                Console.WriteLine("Таблица пустая...");
            }            
        }

        public void ShowById(AppContext db)
        {
            Console.Write("Введите ID книги: ");
            try
            {
                var bookById = new Book();
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {                                        
                    bookById = db.Books.Where(b => b.Id == inputId).FirstOrDefault();
                    try
                    {
                        if (bookById==null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            Console.WriteLine($"{nameof(bookById.Id)}\t\t{nameof(bookById.Title)}\t\t{nameof(bookById.YearOfRealise)}");
                            Console.WriteLine($"{bookById.Id}\t\t{bookById.Title}\t\t{bookById.YearOfRealise}");
                            Console.WriteLine();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Книги с таким ID нет в базе данных...\n");
                    }                                        
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch (NoIdException)
            {
                Console.WriteLine("Книги с таким ID нет в базе данных...\n");
            }            
        }

        public void Update(AppContext db)
        {
            Console.Write("Введите ID книги: ");
            try
            {
                var book = new Book();
                int inputId;
                if (int.TryParse(Console.ReadLine(), out inputId))
                {                    
                    book = db.Books.Where(b => b.Id == inputId).FirstOrDefault();
                    try
                    {
                        if (book==null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            Console.Write("Введите новый год выпуска книги: ");
                            var newYearOfRealiseBook = Console.ReadLine();
                            book.YearOfRealise = newYearOfRealiseBook;
                            db.SaveChanges();
                            Console.WriteLine("Год выпуска изменен...");
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Книги с таким ID нет в базе данных...\n");
                    }                                       
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch (NoIdException)
            {
                Console.WriteLine("Книги с таким ID нет в базе данных...\n");
            }            
        }
    }
}
