using Books.Data.Entities;
using Books.Data.Interfaces;

namespace Books.Data.Repositories
{
    public class AuthorsRepository: IRepository<Author>
    {
        private BooksContext _context;

        public AuthorsRepository(BooksContext context)
        {
            _context = context;
        }

        public Author Delete(Author entity)
        {
            _context.Remove(entity);

            return entity;
        }

        public IQueryable<Author> Get()
        {
            return _context.Authors.AsQueryable();
        }

        public Author? GetById(int id)
        {
            return _context.Authors.First(x => x.Id == id);
        }

        public Author Insert(Author entity)
        {
            _context.Authors.Add(entity);

            return entity;
        }

        public Author Update(Author entity)
        {
            _context.Authors.Update(entity);

            return entity;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
