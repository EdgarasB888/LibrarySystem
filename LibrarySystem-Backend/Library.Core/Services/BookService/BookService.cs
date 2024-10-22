using Library.Core.Services.Generic;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories.BookRepository;
using Library.Infrastructure.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Services.BookService
{
    public class BookService : GenericService<Book>, IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository repository) : base(repository)
        {
            _bookRepository = repository;
        }

        public async Task<IEnumerable<Book>> SearchAsync(string query)
        {
            return await _bookRepository.SearchAsync(query);
        }
    }
}
