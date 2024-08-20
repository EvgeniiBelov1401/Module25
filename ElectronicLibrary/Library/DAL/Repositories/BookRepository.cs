using Library.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public class BookRepository
    {
        public void AddBook(AppContext db)
        {
            Console.Write("Введите название книги: ");
            var bookTitle = Console.ReadLine();
            Console.Write("Введите год написания книги: ");
            var bookYearOfRealise = Console.ReadLine();


                var book = new Book { Title = bookTitle, YearOfRealise=bookYearOfRealise };
                db.Books.Add(book);
                db.SaveChanges();

        }
    }
}
