using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class SelectRepository
    {
        public void ExecuteEx1(AppContext db)
        {
            int yearMin;
            int yearMax;
            Console.WriteLine("Жанры книг:");
            var bookGenre = db.Genres.Where(g => g.Name != string.Empty).ToList();
            foreach (var genre in bookGenre)
            {
                Console.WriteLine($"\t{genre.Name}");
            }
            Console.Write("Выберите жанр: ");
            var choosenGenre = Console.ReadLine();
            Console.Write("Выберите год от: ");
            if (int.TryParse(Console.ReadLine(), out yearMin))
            {
                Console.Write("Выберите год по: ");
                if (int.TryParse(Console.ReadLine(), out yearMax))
                {
                    var selectList = db.Books.Where(y1 => y1.YearOfRealise >= yearMin).Where(y2 => y2.YearOfRealise <= yearMax).Include(g => g.Genre).Where(g => g.Genre.Name == choosenGenre).ToList();
                    if (selectList.Count == 0)
                    {
                        Console.WriteLine("Книг с указанными параметрами нет в базе данных...");
                    }
                    else
                    {
                        foreach (var book in selectList)
                        {
                            Console.WriteLine($"{book.Title}({book.YearOfRealise}) - {book.Genre.Name}");
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Введите корректные данные...");
                }
            }
            else
            {
                Console.WriteLine("Введите корректные данные...");
            }
        }
        public void ExecuteEx2(AppContext db)
        {
            Console.WriteLine("Авторы книг:");
            var bookAuthor = db.Authors.Where(a => a.Name != string.Empty).ToList();
            foreach (var author in bookAuthor)
            {
                Console.WriteLine($"\t{author.Name}");
            }
            Console.Write("Выберите автора: ");
            var choosenAuthor = Console.ReadLine();
            var booksByAuthorCount = db.Books.Include(a => a.Author).Where(a => a.Author.Name == choosenAuthor).ToList().Count();
            if (booksByAuthorCount == 0)
            {
                Console.WriteLine($"Книг автора {choosenAuthor} нет в библиотеке...");
            }
            else
            {
                Console.WriteLine($"В библиотеке книг автора {choosenAuthor}: {booksByAuthorCount} шт.");
            }
        }
        public void ExecuteEx3(AppContext db)
        {
            Console.WriteLine("Жанры книг:");
            var bookGenre = db.Genres.Where(g => g.Name != string.Empty).ToList();
            foreach (var genre in bookGenre)
            {
                Console.WriteLine($"\t{genre.Name}");
            }
            Console.Write("Выберите жанр: ");
            var choosenGenre = Console.ReadLine();
            var booksByGenreCount = db.Books.Include(g => g.Genre).Where(g => g.Genre.Name == choosenGenre).ToList().Count();
            if (booksByGenreCount == 0)
            {
                Console.WriteLine($"Жанра {choosenGenre} нет в библиотеке...");
            }
            else
            {
                Console.WriteLine($"В библиотеке книг жанра {choosenGenre}: {booksByGenreCount} шт.");
            }
        }
        public void ExecuteEx4(AppContext db)
        {
            Console.Write("Введите имя автора: ");
            var choosenAuthor=Console.ReadLine();
            Console.Write("Введите название книги: ");
            var choosenBook = Console.ReadLine();

            var authorToBook = db.Books.Where(b=>b.Title== choosenBook).Include(a=>a.Author).Where(a=>a.Author.Name==choosenAuthor).Any();

            Console.WriteLine($"Наличие в библиотеке книги {choosenBook} автора {choosenAuthor}: {authorToBook}");
        }
        public void ExecuteEx5(AppContext db)
        {
            Console.Write("Введите имя пользователя: ");
            var choosenUser = Console.ReadLine();
            Console.Write("Введите название книги: ");
            var choosenBook = Console.ReadLine();

            var userToBook = db.Users.Where(u=>u.Name==choosenUser).Include(b=>b.Book).Where(b=>b.Book.Title==choosenBook).Any();

            Console.WriteLine($"У клиента {choosenUser} есть книга {choosenBook}: {userToBook}");
        }
        public void ExecuteEx6(AppContext db)
        {
            Console.Write("Введите имя пользователя: ");
            var userName=Console.ReadLine();
            var bookCountOnUser=db.Users.Where(u=>u.Name==userName).Include(b=>b.Book).Count();
            Console.WriteLine($"У клиента {userName} книг в количестве: {bookCountOnUser} шт.");
        }

        public static void AddExerciseList()
        {
            Console.WriteLine("\tСписок дополнительных команд для работы с базой данных:");
            Console.WriteLine($"{Comands.ex1}  -  Получать список книг определенного жанра и вышедших между определенными годами");
            Console.WriteLine($"{Comands.ex2}  -  Получать количество книг определенного автора в библиотеке");
            Console.WriteLine($"{Comands.ex3}  -  Получать количество книг определенного жанра в библиотеке");
            Console.WriteLine($"{Comands.ex4}  -  Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке");
            Console.WriteLine($"{Comands.ex5}  -  Получать булевый флаг о том, есть ли определенная книга на руках у пользователя");
            Console.WriteLine($"{Comands.ex6}  -  Получать количество книг на руках у пользователя");
            Console.WriteLine($"{Comands.ex7}  -  Получение последней вышедшей книги");
            Console.WriteLine($"{Comands.ex8}  -  Получение списка всех книг, отсортированного в алфавитном порядке по названию");
            Console.WriteLine($"{Comands.ex9}  -  Получение списка всех книг, отсортированного в порядке убывания года их выхода");
        }
    }
}
