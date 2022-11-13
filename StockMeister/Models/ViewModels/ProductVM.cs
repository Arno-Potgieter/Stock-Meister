namespace StockMeister.Models.ViewModels
{
    public class ProductVM
    {
        public int? Id { get; set; }

        public string? ProductName { get; set; }

        public string? ProductPrice { get; set; }

        public int? CategoryId { get; set; }
    }
}
