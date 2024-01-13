using Books.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BooksContext: DbContext
    {
        public string DbPath { get; }

        public BooksContext() : base()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "BooksSite.db");
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<AuthorBook> AuthorsBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()
                        .HasKey(ab => new { ab.BookId, ab.AuthorId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.AuthorBooks)
                .HasForeignKey(bc => bc.AuthorId);
        }
    }
}
