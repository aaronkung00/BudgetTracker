using AaronKung.BudgetTracker.Core.Models.Request_model;
using AaronKung.BudgetTracker.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AaronKung.BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var response = await _expenseService.GetAllExpenses();
            return Ok(response);
        }


        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetExpensesByUserId(int userId)
        {
            var response = await _expenseService.GetExpensesByUserId(userId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpenditure(ExpenditureRequestModel expenditureRequestModel)
        {
            var response = await _expenseService.AddExpense(expenditureRequestModel);
            return Ok(response); 
        }


        [HttpPut]
        public async Task<IActionResult> UpdateExpenditure(ExpenditureRequestModel expenditureRequestModel)
        {
             await _expenseService.UpdateExpense(expenditureRequestModel);
            return Ok(expenditureRequestModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExpenditure(int id)
        {
            await _expenseService.DeleteExpense(id);
            return Ok("Delete Successed!");
        }







    }
}
