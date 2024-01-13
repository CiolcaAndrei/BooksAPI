using Books.Data.Entities;
using Books.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Books.Data.Repositories
{
    public class BooksRepository : IRepository<Book>
    {
        private BooksContext _context;
        public BooksRepository(BooksContext context)
        {
            _context = context;
        }

        public Book Delete(Book entity)
        {
            _context.Books.Remove(entity);

            return entity;
        }

        public IQueryable<Book> Get()
        {
            return _context.Books.AsQueryable().Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author);
        }

        public Book? GetById(int id)
        {
            return _context.Books.Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author).FirstOrDefault(b => b.Id == id);
        }

        public Book Insert(Book entity)
        {
            var existingAuthors = entity.AuthorBooks.Select(ab => ab.Author);

           
            foreach(var existingAuthor in existingAuthors)
            {
                _context.Attach(existingAuthor);
            }

            _context.Books.Add(entity);

            return entity;
        }

        public Book Update(Book entity)
        {
            var existingRelationships = _context.AuthorsBooks.Where(ab => ab.BookId == entity.Id);

            _context.AuthorsBooks.RemoveRange(existingRelationships);

            foreach(var relationship in entity.AuthorBooks)
            {
                var newRelationship = new AuthorBook
                {
                    BookId = entity.Id,
                    AuthorId = relationship.Author.Id
                };

                _context.AuthorsBooks.Add(newRelationship);
            }

            entity.AuthorBooks.Clear();

            _context.Update(entity);

            return entity;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
