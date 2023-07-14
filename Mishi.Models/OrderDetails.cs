using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mishi.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int MenuItedId { get; set; }

        [ForeignKey("MenuItedId")]
        [ValidateNever]
        public MenuItem MenuItem { get; set; }

        public int Count { get; set; } = 1;
        [Required]
        public double Price { get; set; }

        public string Name { get; set;}
    }
}
