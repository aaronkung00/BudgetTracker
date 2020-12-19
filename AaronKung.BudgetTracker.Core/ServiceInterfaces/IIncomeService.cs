using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.Models.Response_model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Core.ServiceInterfaces
{
    public interface IIncomeService
    {
        Task<IncomeResponseModel> AddIncome(IncomeRequestModel incomeRequest);
        Task UpdateIncome(IncomeRequestModel incomeRequest);
        Task DeleteIncome(int income_Id);
        Task<IEnumerable<IncomeResponseModel>> GetAllIncomes();
        Task<IEnumerable<Income>> GetIncomesByUserId(int userId);
        Task<decimal> GetSumOfIncomes(int id);
    }
}
