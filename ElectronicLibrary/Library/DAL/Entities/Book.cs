using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? YearOfRealise { get; set; }

        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

        public List<User>? Users { get; set; }


        
    }
}
