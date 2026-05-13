namespace Biblioteca.Server.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genres { get; set; }
        
        public int state { get; set; }
        //aca me surge la duda, vamos a tener 1 de cada libro? 0 seria para rentado e 1 para alquilado PREGUNTAR
    }
}
