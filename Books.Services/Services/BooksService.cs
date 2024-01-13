using Books.Data.Entities;
using Books.Data.Interfaces;

namespace Books.Services.Services
{
    public class BooksService
    {
        private IRepository<Book> _booksRepository;

        public BooksService(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _booksRepository.Get();
        }

        public Book Insert(Book book)
        {
            _booksRepository.Insert(book);
            _booksRepository.SaveChanges();

            return book;
        }

        public Book Update(Book book)
        {
            _booksRepository.Update(book);
            _booksRepository.SaveChanges();

            return book;
        }

        public Book Delete(Book book)
        {
            var bookDeleted = _booksRepository.Delete(book);
            _booksRepository.SaveChanges();

            return bookDeleted;
        }

        public Book? GetById(int id)
        {
            var result = _booksRepository.GetById(id);

            return result;
        }
    }
}
