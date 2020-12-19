using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.Models.Response_model;
using AaronKung.BudgetTracker.Core.RepositoryInterfaces;
using AaronKung.BudgetTracker.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Infrastructure.Services
{
    public class ExpenseService : IExpenseService
    {

        private readonly IAsyncRepository<Expenditure> _expenseAsyncRepository;

        public ExpenseService(IAsyncRepository<Expenditure> expenseAsyncRepository)
        {
            _expenseAsyncRepository = expenseAsyncRepository;
        }

        public async Task<ExpenseResponseModel> AddExpense(ExpenditureRequestModel expenditureRequest)
        {
            var expense = new Expenditure 
            {
                UserId = expenditureRequest.UserId,
                Amount = expenditureRequest.Amount,
                Description = expenditureRequest.Description,
                ExpDate = expenditureRequest.Date,
                Remarks = expenditureRequest.Remarks
            };


            await _expenseAsyncRepository.AddAsync(expense);

            var response = new ExpenseResponseModel
            {
                UserId = expenditureRequest.UserId,
                Amount = expenditureRequest.Amount,
                Description = expenditureRequest.Description,
                Date = expenditureRequest.Date,
                Remarks = expenditureRequest.Remarks
            };

            return response;
        }

        public async Task DeleteExpense(int exp_id)
        {
            var expenses = await _expenseAsyncRepository.ListAsync(e => e.Id == exp_id);
            await _expenseAsyncRepository.DeleteAsync(expenses.First());
        }

        public async Task<IEnumerable<ExpenseResponseModel>> GetAllExpenses()
        {
            var expenses = await _expenseAsyncRepository.ListAllAsync();
            List<ExpenseResponseModel> response = new List<ExpenseResponseModel>();
            foreach (var exp in expenses)
            {
                if(exp.ExpDate != null)
                {
                    response.Add(new ExpenseResponseModel
                    {
                        id = exp.Id,
                        UserId = exp.UserId,
                        Amount = exp.Amount,
                        Date = exp.ExpDate.Value,
                        Description = exp.Description,
                        Remarks = exp.Remarks
                    });
                }
            }

            return response;
        }

        public async Task<IEnumerable<ExpenseResponseModel>> GetExpensesByUserId(int userId)
        {
            var expenses = await _expenseAsyncRepository.ListAsync(e => e.UserId == userId);
            List<ExpenseResponseModel> response = new List<ExpenseResponseModel>();
            foreach (var exp in expenses)
            {
                if (exp.ExpDate != null)
                {
                    response.Add(new ExpenseResponseModel
                    {
                        id = exp.Id,
                        UserId = exp.UserId,
                        Amount = exp.Amount,
                        Date = exp.ExpDate.Value,
                        Description = exp.Description,
                        Remarks = exp.Remarks
                    });
                }
            }

            return response;
        }

        public async Task<decimal> GetSumOfExpenses(int userId)
        {
            var expenses = await _expenseAsyncRepository.ListAsync(e => e.UserId == userId);
            decimal sum = 0;
            foreach (var exp in expenses)
            {
                sum += exp.Amount;
            }

            return sum;
        }

        public async Task UpdateExpense(ExpenditureRequestModel expenditureRequest)
        {
            var expense = new Expenditure
            {
                Id = expenditureRequest.Id,
                UserId = expenditureRequest.UserId,
                Amount = expenditureRequest.Amount,
                Description = expenditureRequest.Description,
                ExpDate = expenditureRequest.Date,
                Remarks = expenditureRequest.Remarks
            };

            await _expenseAsyncRepository.UpdateAsync(expense);
        }
    }
}
