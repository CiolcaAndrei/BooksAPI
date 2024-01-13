using Book.DTOs;
using Books.Data.Entities;

namespace Book.Profiles
{
    public class BookProfile
    {
        public static BookDTO Get(Books.Data.Entities.Book book)
        {
            var result = new BookDTO()
            {
                Id = book.Id,
                CoverPath = book.CoverPath,
                Title = book.Title,
                Description = book.Description,
                Authors = new List<AuthorDTO>()
            };

            foreach(var author in book.AuthorBooks.Select(ab => ab.Author))
            {
                var authorDTO = new AuthorDTO()
                {
                    Id = author.Id,
                    Name = author.Name
                };

                result.Authors.Add(authorDTO);
            }

            return result;
        }

        public static Books.Data.Entities.Book Get(BookDTO bookDTO)
        {
            var result = new Books.Data.Entities.Book()
            {
                Id = bookDTO.Id,
                CoverPath = bookDTO.CoverPath,
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AuthorBooks = new List<AuthorBook>()
            };

            foreach(var a in bookDTO.Authors)
            {
                var author = new Author()
                {
                    Id = a.Id,
                    Name = a.Name
                };

                result.AuthorBooks.Add(new AuthorBook()
                {
                    Author = author
                });
            }

            return result;
        }
    }
}
