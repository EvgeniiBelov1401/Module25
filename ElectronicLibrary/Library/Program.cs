using Microsoft.EntityFrameworkCore;
using Library.DAL;
using AppContext = Library.DAL.AppContext;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Linq;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppContext())
            {
                var user = new UserRepository();
                var book = new BookRepository(db);
                var select = new SelectRepository();
                
                while (true)
                {
                    Console.WriteLine("\tСписок команд для работы с базой данных:");
                    Console.WriteLine($"{Comands.adduser}  -  добавить нового пользователя");
                    Console.WriteLine($"{Comands.addbook}  -  добавить новую книгу");
                    Console.WriteLine($"{Comands.dropuser}  -  удалить пользователя");
                    Console.WriteLine($"{Comands.dropbook}  -  удалить книгу");
                    Console.WriteLine($"{Comands.showusers}  -  показать всех пользователей");
                    Console.WriteLine($"{Comands.showbooks}  -  показать все книги");
                    Console.WriteLine($"{Comands.showuserbyid}  -  показать пользователя по ID");
                    Console.WriteLine($"{Comands.showbookbyid}  -  показать книгу по ID");
                    Console.WriteLine($"{Comands.updateuser}  -  изменить имя пользователя");
                    Console.WriteLine($"{Comands.updatebook}  -  изменить год выпуска книги");
                    if (db.Users.Count()>0 || db.Books.Count()>0)
                    {
                        SelectRepository.AddExerciseList();
                    }
                    Console.Write("\nВведите команду: ");
                    var comand=Console.ReadLine();

                    switch(comand)
                    {
                        case nameof(Comands.adduser):
                            user.Add(db);
                            break;
                        case nameof(Comands.addbook):
                            book.Add(db);
                            break;
                        case nameof(Comands.dropuser):
                            user.Drop(db);
                            break;
                        case nameof(Comands.dropbook):
                            book.Drop(db);
                            break;
                        case nameof(Comands.showusers):
                            user.ShowAll(db);
                            break;
                        case nameof(Comands.showbooks):
                            book.ShowAll(db);
                            break;
                        case nameof(Comands.showuserbyid):
                            user.ShowById(db);
                            break;
                        case nameof(Comands.showbookbyid):
                            book.ShowById(db);
                            break;
                        case nameof(Comands.updateuser):
                            user.Update(db);
                            break;
                        case nameof(Comands.updatebook):
                            book.Update(db);
                            break;
                        case nameof(Comands.ex1):
                            select.ExecuteEx1(db);
                            break;
                        case nameof(Comands.ex2):
                            select.ExecuteEx2(db);
                            break;
                        case nameof(Comands.ex3):
                            select.ExecuteEx3(db);
                            break;
                        case nameof(Comands.ex4):
                            select.ExecuteEx4(db);
                            break;
                        case nameof(Comands.ex5):
                            select.ExecuteEx5(db);
                            break;
                        default:
                            Console.WriteLine("Введите корректную команду!!!");
                            break;
                    }
                    Console.ReadLine();
                    Console.Clear();
                }
            }           
        }
    }
}
