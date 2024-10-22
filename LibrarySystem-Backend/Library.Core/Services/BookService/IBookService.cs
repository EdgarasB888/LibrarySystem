using Library.Core.Services.Generic;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.BookService
{
    public interface IBookService : IGenericService<Book>
    {
        Task<IEnumerable<Book>> SearchAsync(string query);
    }
}
