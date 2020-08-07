namespace WebFrontend.Model
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string name { get; set; }

        public string ISBN { get; set; }
        public Book Book { get; set; }
    }
}
