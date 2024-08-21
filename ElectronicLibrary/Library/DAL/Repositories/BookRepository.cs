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
        public BookRepository(AppContext db)
        {
            FillAthorTable(db);
            FillGenreTable(db);
        }
        public void Add(AppContext db)
        {
            Console.Write("Введите название книги: ");
            var bookTitle = Console.ReadLine();
            Console.Write("Введите год написания книги: ");
            var bookYearOfRealise = Console.ReadLine();
            #region Добавление Автора и Жанра из таблиц Authors и Genres
            Console.Write("Введите ID автора книги: ");
            int authorBookId;
            var searchAuthor = new Author();
            var book=new Book();    
            try
            {
                if (int.TryParse(Console.ReadLine(), out authorBookId))
                {
                    searchAuthor = db.Authors.Where(a => a.Id == authorBookId).FirstOrDefault();
                    try
                    {
                        if (searchAuthor == null)
                        {
                            throw new NullReferenceException();
                        }
                        else
                        {
                            book.AuthorId=searchAuthor.Id;

                            Console.Write("Введите ID ажанра книги: ");
                            int genreBookId;
                            var searchGenre = new Genre();
                            try
                            {
                                if (int.TryParse(Console.ReadLine(), out genreBookId))
                                {
                                    searchGenre = db.Genres.Where(g => g.Id == genreBookId).FirstOrDefault();
                                    try
                                    {
                                        if (searchGenre == null)
                                        {
                                            throw new NullReferenceException();
                                        }
                                        else
                                        {
                                            book = new Book { Title = bookTitle, YearOfRealise = bookYearOfRealise, AuthorId = book.AuthorId, GenreId = searchGenre.Id };
                                            db.Books.Add(book);
                                            db.SaveChanges();
                                            Console.WriteLine("Добавлена новая книга...");
                                        }
                                    }
                                    catch (NullReferenceException)
                                    {
                                        Console.WriteLine("Жанра с таким ID нет в базе данных...\n");
                                    }
                                }
                                else
                                {
                                    throw new NoIdException();
                                }
                            }
                            catch (NoIdException)
                            {
                                Console.WriteLine("Жанра с таким ID нет в базе данных...\n");
                            }
                        }
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Автора с таким ID нет в базе данных...\n");
                    }
                }
                else
                {
                    throw new NoIdException();
                }
            }
            catch (NoIdException)
            {
                Console.WriteLine("Автора с таким ID нет в базе данных...\n");
            }
            #endregion
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

        private static void FillAthorTable(AppContext db)
        {
            var author1 = new Author {Name="Александр Пушкин" };
            var author2 = new Author { Name = "Михаил Булгаков" };
            var author3 = new Author { Name = "Федор Достоевский" };
            var author4 = new Author { Name = "Конан Дойл" };
            var author5 = new Author { Name = "Александр Дюма" };
            var author6 = new Author { Name = "Валентин Пикуль" };
            var author7 = new Author { Name = "Агата Кристи" };
            var author8 = new Author { Name = "Иван Тургенев" };
            var author9 = new Author { Name = "Лев Толстой" };
            var author10 = new Author { Name = "Марио Пьюзо" };

            db.Authors.AddRange(author1, author2, author3, author4, author5, author6, author7, author8, author9, author10);
            db.SaveChanges();
        }

        private static void FillGenreTable(AppContext db)
        {
            var genre1 = new Genre {Name="Детектив"};
            var genre2 = new Genre { Name = "Исторический роман" };
            var genre3 = new Genre { Name = "Прикличения" };
            var genre4 = new Genre { Name = "Криминальный роман" };
            var genre5 = new Genre { Name = "Трагедия" };

            db.Genres.AddRange(genre1,genre2,genre3,genre4,genre5);
            db.SaveChanges();
        }
    }
}
