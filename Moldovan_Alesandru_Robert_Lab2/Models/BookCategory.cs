
namespace Moldovan_Alexandru_Robert_Lab2.Models
{
    public class BookCategory
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }
        public Category Category {  get; set; }
        
        public int BookID {  get; set; }
        public Book Book { get; set; }

    }
}