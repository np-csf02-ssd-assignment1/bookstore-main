namespace WebFrontend.Model
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string name { get; set; }

        public string ISBN { get; set; }
        public Book Book { get; set; }
    }
}
