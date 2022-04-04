using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockMeister.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }
    }
}
