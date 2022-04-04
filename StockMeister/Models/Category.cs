using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMeister.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
    }
}
