using AaronKung.BudgetTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AaronKung.BudgetTracker.Core.Models.Response_model
{
    public class UserDetailResponseModel : UserResponseModel
    {

        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }

        public ICollection<Income> Incomes;

        public ICollection<Expenditure> Expenditures;


    }
}
