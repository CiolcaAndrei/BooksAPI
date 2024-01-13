namespace Book.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string CoverPath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<AuthorDTO> Authors { get; set; }
    }
}
