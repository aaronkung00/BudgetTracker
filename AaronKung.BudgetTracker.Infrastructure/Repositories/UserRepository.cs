using AaronKung.BudgetTracker.Core.Entities;
using AaronKung.BudgetTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.Infrastructure.Repositories
{
    public class UserRepository : EF_Repository<User> 
    {

        public UserRepository(BudgetTrackerDbContext dbContext) : base(dbContext)
        {

        }


        public override async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Expenditures).Include(u => u.Incomes).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }


    }
}
