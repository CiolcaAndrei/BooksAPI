using System.ComponentModel.DataAnnotations;

namespace Books.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string CoverPath { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public virtual List<AuthorBook> AuthorBooks { get; set; }    
    }
}
