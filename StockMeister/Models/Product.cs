using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMeister.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string ProductPrice { get; set; }

        [DisplayName("Select a Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }

        public int? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company? Company { get; set; }
    }
}
