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
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userAsyncRepository;

        public UserService(IAsyncRepository<User> userAsyncRepository)
        {
            _userAsyncRepository = userAsyncRepository;
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
        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var users = await _userAsyncRepository.ListAllAsync();

            List<UserResponseModel> response = new List<UserResponseModel>();

            foreach (var user in users)
            {
                response.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName
                });   
            }

            return response;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userAsyncRepository.GetByIdAsync(id);
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
