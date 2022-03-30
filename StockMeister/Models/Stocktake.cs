using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMeister.Models
{
    public class Stocktake
    {
        public int Id { get; set; }

        public int CurrentStock { get; set; }

        public int ActualStock { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
