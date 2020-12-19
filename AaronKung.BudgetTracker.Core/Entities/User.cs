using AaronKung.BudgetTracker.Core.Models.Response_model;
using System;
using System.Collections.Generic;
using System.Text;


namespace AaronKung.BudgetTracker.Core.Entities
{
    public class User
    {
        
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? JoinedOn { get; set; }


        //Navigator
        public ICollection<Income> Incomes;
        public ICollection<Expenditure> Expenditures;

    }
}
