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
    public class IncomeService : IIncomeService
    {
        private readonly IAsyncRepository<Income> _incomeAsyncRepository;

        public IncomeService(IAsyncRepository<Income> incomeAsyncRepository)
        {
            _incomeAsyncRepository = incomeAsyncRepository;
        }

        public async Task<IncomeResponseModel> AddIncome(IncomeRequestModel incomeRequest)
        {
            var income = new Income 
            { 
               UserId = incomeRequest.UserId,
               Amount = incomeRequest.Amount,
               Description = incomeRequest.Description,
               IncomeDate = incomeRequest.Date,
               Remarks = incomeRequest.Remarks
            };

            await _incomeAsyncRepository.AddAsync(income);


            var response = new IncomeResponseModel 
            {
                UserId = incomeRequest.UserId,
                Amount = incomeRequest.Amount,
                Description = incomeRequest.Description,
                IncomeDate = incomeRequest.Date,
                Remarks = incomeRequest.Remarks
            };

            return response;
        }

        public async Task DeleteIncome(int income_Id)
        {
            var income = await _incomeAsyncRepository.ListAsync(i => i.Id == income_Id);
            await _incomeAsyncRepository.DeleteAsync(income.First());
        }

        public async Task<IEnumerable<IncomeResponseModel>> GetAllIncomes()
        {
            var incomes = await _incomeAsyncRepository.ListAllAsync();
            List<IncomeResponseModel> response = new List<IncomeResponseModel>();

            foreach (var income in incomes)
            {
                response.Add( new IncomeResponseModel
                {
                   Id = income.Id,
                   UserId = income.UserId,
                   Amount = income.Amount,
                   Description = income.Description,
                   IncomeDate = income.IncomeDate,
                   Remarks = income.Remarks
                });
            }


            return response;
        }

        public async Task<IEnumerable<IncomeResponseModel>> GetIncomesByUserId(int userId)
        {
            var incomes = await _incomeAsyncRepository.ListAsync(i => i.UserId == userId);
            List<IncomeResponseModel> response = new List<IncomeResponseModel>();

            foreach (var income in incomes)
            {
                response.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }


            return response;
        }

        public async Task<decimal> GetSumOfIncomes(int id)
        {
            var incomes = await _incomeAsyncRepository.ListAsync(i => i.UserId == id);
            decimal sum = 0;

            foreach (var income in incomes)
            {
                sum += income.Amount;
            }

            return sum;
        }

        public async Task UpdateIncome(IncomeRequestModel incomeRequest)
        {
            var income = new Income
            {
                Id = incomeRequest.id,
                UserId = incomeRequest.UserId,
                Amount = incomeRequest.Amount,
                Description = incomeRequest.Description,
                IncomeDate = incomeRequest.Date,
                Remarks = incomeRequest.Remarks
            };

            await _incomeAsyncRepository.UpdateAsync(income);
        }
    }
}
