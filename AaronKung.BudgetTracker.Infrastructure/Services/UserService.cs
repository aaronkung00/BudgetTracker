using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.Models.Response_model;
using AaronKung.BudgetTracker.Core.RepositoryInterfaces;
using AaronKung.BudgetTracker.Core.ServiceInterfaces;
using AaronKung.BudgetTracker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userAsyncRepository;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;

        //test
     
        public UserService(IAsyncRepository<User> userAsyncRepository, IIncomeService incomeService, IExpenseService expenseService)
        {
            _userAsyncRepository = userAsyncRepository;
            _incomeService = incomeService;
            _expenseService = expenseService;
  
        }

        public async Task<UserResponseModel> AddUser(AddUserRequestModel addUserRequestModel)
        {
        
           var user = new User
            {
                FullName = addUserRequestModel.FullName,
                Email = addUserRequestModel.Email,
                Password = addUserRequestModel.Password,
                JoinedOn = DateTime.Now
            };

            await _userAsyncRepository.AddAsync(user);

            var response = new UserResponseModel
            {
                Email = addUserRequestModel.Email,
                FullName = addUserRequestModel.FullName,
            };

            return response;
        }

        // Delete User by Id
        public async Task DeleteUser(int userId)
        {
            var userEF = await _userAsyncRepository.ListAsync((u => u.Id == userId));
            // Delete the first result.
            await _userAsyncRepository.DeleteAsync(userEF.First());
        }

        // To-do Add Total expenses / incomes to response model
        public async Task<IEnumerable<UserDetailResponseModel>> GetAllUsers()
        {
            var users = await _userAsyncRepository.ListAllAsync();


            List<UserDetailResponseModel> response = new List<UserDetailResponseModel>();

            foreach (var user in users)
            {
                var user_incomes = await _incomeService.GetIncomesByUserId(user.Id);
                var user_expenses = await _expenseService.GetExpensesByUserId(user.Id);

                user.Incomes = user_incomes.ToList();
                user.Expenditures = user_expenses.ToList();

                var userTotalIncome = user_incomes.Select(i => i.Amount).DefaultIfEmpty(0).Sum();
                var userTotalExpense = user_expenses.Select(e => e.Amount).DefaultIfEmpty(0).Sum();

                var responseModel = new UserDetailResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    TotalExpense = userTotalExpense,
                    TotalIncome = userTotalIncome,
                    Expenditures = new List<Expenditure>(),
                    Incomes = new List<Income>()
                };

                if (user_incomes != null)
                {
                    foreach (var income in user_incomes)
                    {
                        responseModel.Incomes.Add(income);
                    }
                }

                if(user_expenses != null)
                {
                    foreach (var exp in user_expenses)
                    {
                        responseModel.Expenditures.Add(exp);
                    }
                }

                response.Add(responseModel);

            }

            return response;
        }

        public async Task<UserDetailResponseModel> GetUserById(int id)
        {
            var user  = await _userAsyncRepository.GetByIdAsync(id);

            var user_incomes = await _incomeService.GetIncomesByUserId(user.Id);
            var user_expenses = await _expenseService.GetExpensesByUserId(user.Id);

            user.Incomes = user_incomes.ToList();
            user.Expenditures = user_expenses.ToList();

            var userTotalIncome = user_incomes.Select(i => i.Amount).DefaultIfEmpty(0).Sum();
            var userTotalExpense = user_expenses.Select(e => e.Amount).DefaultIfEmpty(0).Sum();

            var responseModel = new UserDetailResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                TotalExpense = userTotalExpense,
                TotalIncome = userTotalIncome,
                Expenditures = new List<Expenditure>(),
                Incomes = new List<Income>()
            };


            if (user_incomes != null)
            {
                foreach (var income in user_incomes)
                {
                    responseModel.Incomes.Add(income);
                }
            }

            if (user_expenses != null)
            {
                foreach (var exp in user_expenses)
                {
                    responseModel.Expenditures.Add(exp);
                }
            }

            return responseModel;
            //return await _userRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsExist(string email)
        {
            return await _userAsyncRepository.GetExistsAsync(u => u.Email == email);
        }

        public async Task UpdateUser(AddUserRequestModel addUserRequestModel)
        {
            var user = new User
            {
                Id = addUserRequestModel.Id,
                FullName = addUserRequestModel.FullName,
                Email = addUserRequestModel.Email,
                Password = addUserRequestModel.Password,
                JoinedOn = addUserRequestModel.JoinedOn


            };

    
            await _userAsyncRepository.UpdateAsync(user);
        }
    }
}
