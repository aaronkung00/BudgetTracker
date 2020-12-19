using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.Models.Response_model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Core.ServiceInterfaces
{
    public interface IExpenseService
    {
        Task<ExpenseResponseModel> AddExpense(ExpenditureRequestModel expenditureRequest);
        Task UpdateExpense(ExpenditureRequestModel expenditureRequest);
        Task DeleteExpense(int exp_Id);
        Task<IEnumerable<ExpenseResponseModel>> GetAllExpenses();

        Task<IEnumerable<Expenditure>> GetExpensesByUserId(int userId);

        Task<decimal> GetSumOfExpenses(int id);
    }
}
