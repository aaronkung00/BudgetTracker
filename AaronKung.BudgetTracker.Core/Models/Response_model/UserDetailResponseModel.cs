using System;
using System.Collections.Generic;
using System.Text;

namespace AaronKung.BudgetTracker.Core.Models.Response_model
{
    public class UserDetailResponseModel : UserResponseModel
    {

        public int TotalIncome { get; set; }
        public int TotalExpense { get; set; }

    }
}
