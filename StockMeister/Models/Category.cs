using System.ComponentModel.DataAnnotations;

namespace StockMeister.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
    }
}
