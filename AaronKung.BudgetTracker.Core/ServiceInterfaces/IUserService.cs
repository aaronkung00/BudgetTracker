using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.Models.Response_model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> AddUser(AddUserRequestModel addUserRequestModel);
        Task UpdateUser(AddUserRequestModel addUserRequestModel);
        Task DeleteUser(int userId);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task<bool> IsExist(string email);
        Task<User> GetUserById(int id);
    }
}
