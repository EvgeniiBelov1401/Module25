using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public interface IRepository
    {
        void Add(AppContext db);
        void Drop(AppContext db);

        void ShowAll(AppContext db);
    }
}
