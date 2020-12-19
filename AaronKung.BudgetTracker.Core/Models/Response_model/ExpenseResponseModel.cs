using System;
using System.Collections.Generic;
using System.Text;

namespace AaronKung.BudgetTracker.Core.Models.Response_model
{
    public class ExpenseResponseModel
    {
        public int id { get; set; }
        public int? UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
    }
}
