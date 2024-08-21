using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
