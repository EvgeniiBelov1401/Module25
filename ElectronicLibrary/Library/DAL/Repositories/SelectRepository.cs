﻿using Microsoft.EntityFrameworkCore;
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
            var bookGenre = db.Genres.Where(g=>g.Name!=string.Empty).ToList();
            foreach (var genre in bookGenre)
            {
                Console.WriteLine($"\t{genre.Name}");
            }
            Console.Write("Выберите жанр: ");
            var choosenGenre=Console.ReadLine();
            Console.Write("Выберите год от: ");
            if (int.TryParse(Console.ReadLine(),out yearMin))
            {
                Console.Write("Выберите год по: ");
                if (int.TryParse(Console.ReadLine(), out yearMax))
                {
                    var selectList = db.Books.Where(y1=>y1.YearOfRealise>=yearMin).Where(y2=>y2.YearOfRealise<=yearMax).Include(g => g.Genre).Where(g => g.Genre.Name == choosenGenre).ToList();
                    foreach (var book in selectList)
                    {
                        Console.WriteLine($"{book.Title}\t{book.YearOfRealise}\t{book.Genre}");
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
