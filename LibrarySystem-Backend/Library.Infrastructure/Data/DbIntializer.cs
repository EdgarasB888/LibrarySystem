using Library.Domain.Entities;
using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Data
{
    public static class DbIntializer
    {
        public static void Initialize(LibraryDbContext context)
        {
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book { BookId = 1, Name = "The Great Gatsby", Year = 1925, PictureUrl = "https://m.media-amazon.com/images/I/81QuEGw8VPL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 2, Name = "1984", Year = 1949, PictureUrl = "https://m.media-amazon.com/images/I/61NAx5pd6XL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 3, Name = "To Kill a Mockingbird", Year = 1960, PictureUrl = "https://m.media-amazon.com/images/I/81aY1lxk+9L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 4, Name = "Brave New World", Year = 1932, PictureUrl = "https://m.media-amazon.com/images/I/81zE42gT3xL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 5, Name = "Moby-Dick", Year = 1851, PictureUrl = "https://m.media-amazon.com/images/I/71d5wo+-MuL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 6, Name = "War and Peace", Year = 1869, PictureUrl = "https://m.media-amazon.com/images/I/71wXZB-VtBL.jpg" },
                    new Book { BookId = 7, Name = "The Catcher in the Rye", Year = 1951, PictureUrl = "https://m.media-amazon.com/images/I/8125BDk3l9L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 8, Name = "The Hobbit", Year = 1937, PictureUrl = "https://m.media-amazon.com/images/I/712cDO7d73L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 9, Name = "Pride and Prejudice", Year = 1813, PictureUrl = "https://m.media-amazon.com/images/I/81NLDvyAHrL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 10, Name = "The Lord of the Rings", Year = 1954, PictureUrl = "https://m.media-amazon.com/images/I/7125+5E40JL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 11, Name = "Crime and Punishment", Year = 1866, PictureUrl = "https://m.media-amazon.com/images/I/81bAXZAp-GL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 12, Name = "The Odyssey", Year = -800, PictureUrl = "https://m.media-amazon.com/images/I/81g0AATkO9L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 13, Name = "Frankenstein", Year = 1818, PictureUrl = "https://m.media-amazon.com/images/I/81z7E0uWdtL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 14, Name = "The Iliad", Year = -760, PictureUrl = "https://m.media-amazon.com/images/I/71FVVdj9w4L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 15, Name = "Jane Eyre", Year = 1847, PictureUrl = "https://m.media-amazon.com/images/I/81pwJjgcwwL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 16, Name = "Wuthering Heights", Year = 1847, PictureUrl = "https://m.media-amazon.com/images/I/81unikMK30L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 17, Name = "Dracula", Year = 1897, PictureUrl = "https://m.media-amazon.com/images/I/71yhG9std-L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 18, Name = "Les Misérables", Year = 1862, PictureUrl = "https://m.media-amazon.com/images/I/914ou95ewEL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 19, Name = "Anna Karenina", Year = 1877, PictureUrl = "https://m.media-amazon.com/images/I/61hlBIot81L._UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 20, Name = "The Brothers Karamazov", Year = 1880, PictureUrl = "https://m.media-amazon.com/images/I/71OZJsgZzQL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 21, Name = "The Divine Comedy", Year = 1320, PictureUrl = "https://m.media-amazon.com/images/I/81SgX6pVaWL.jpg" },
                    new Book { BookId = 22, Name = "Madame Bovary", Year = 1857, PictureUrl = "https://m.media-amazon.com/images/I/817vtouD0oL._UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 23, Name = "Don Quixote", Year = 1605, PictureUrl = "https://m.media-amazon.com/images/I/71mbJoazlCL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 24, Name = "The Picture of Dorian Gray", Year = 1890, PictureUrl = "https://m.media-amazon.com/images/I/81yNWdTnoUL._UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 25, Name = "One Hundred Years of Solitude", Year = 1967, PictureUrl = "https://m.media-amazon.com/images/I/81dy4cfPGuL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 26, Name = "Fahrenheit 451", Year = 1953, PictureUrl = "https://m.media-amazon.com/images/I/61l8LHt4MeL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 27, Name = "The Metamorphosis", Year = 1915, PictureUrl = "https://m.media-amazon.com/images/I/61nlvcmPkGL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 28, Name = "Ulysses", Year = 1922, PictureUrl = "https://m.media-amazon.com/images/I/41Rb-Kz6v1L._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 29, Name = "The Count of Monte Cristo", Year = 1844, PictureUrl = "https://m.media-amazon.com/images/I/811iBn28JdL._AC_UF1000,1000_QL80_.jpg" },
                    new Book { BookId = 30, Name = "Great Expectations", Year = 1861, PictureUrl = "https://m.media-amazon.com/images/I/81nfypxJO6L._UF1000,1000_QL80_.jpg" }
                );

                context.SaveChanges();
            }
        }
    }
}
