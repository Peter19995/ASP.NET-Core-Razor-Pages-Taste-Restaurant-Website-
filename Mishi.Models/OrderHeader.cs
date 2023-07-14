using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mishi.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString =  "{0:C}")]
        [DisplayName("Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [DisplayName("Pick Up Time")]
        public DateTime PickUpTime { get; set; }


        [Required]
        [ValidateNever]
        public DateTime PickUpDate { get; set; }

        public string Status { get; set; }

        public string? Comments { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }
        [Required]
		[DisplayName("Picker Phone Number")]
        public string PickUpName { get; set; }

		[Required]
		[DisplayName("Phone Number")]
        public string PickUpPhoneNumber { get; set; }

    }
}
