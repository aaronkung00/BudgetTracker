using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AaronKung.BudgetTracker.Core.Models.Request_model
{
    public class IncomeRequestModel
    {
        public int id { get; set; }
        public int? UserId {get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }

    }
}
