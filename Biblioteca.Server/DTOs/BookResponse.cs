namespace Biblioteca.Server.DTOs
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
