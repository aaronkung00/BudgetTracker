using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AaronKung.BudgetTracker.Core.Models.Request_model
{
    public class AddUserRequestModel
    {

        public int Id{ get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Make sure is password right length", MinimumLength = 5)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{5,}$", ErrorMessage = "Password Should have minimum 5 with at least one upper, lower, number and special character")]
        public string Password { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        public DateTime? JoinedOn { get; set; }
    }
}
