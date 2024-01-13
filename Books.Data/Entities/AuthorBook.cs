namespace Books.Data.Entities
{
    public class AuthorBook
    {
        public int? AuthorId { get; set; }
        public int? BookId { get; set; }
        public Author? Author { get; set; } = null!;
        public Book? Book { get; set; } = null!;
    }
}
