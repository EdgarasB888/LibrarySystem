using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories.BookRepository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> SearchAsync(string query)
        {
            query = query.ToLower();

            return await _context.Books
                .Where(b => b.Name.ToLower().Contains(query) ||
                            b.Year.ToString().Contains(query))
                .ToListAsync();
        }
    }
}
