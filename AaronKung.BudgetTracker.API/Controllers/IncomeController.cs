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
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            var incomes = await _incomeService.GetAllIncomes();

            return Ok(incomes);
        }

        [HttpGet("{UserId:int}")]
        public async Task<IActionResult> GetIncomeByUserId(int UserId)
        {
            var incomes = await _incomeService.GetIncomesByUserId(UserId);
            return Ok(incomes);
        }


        [HttpPost]
        public async Task<IActionResult> AddIncome(IncomeRequestModel incomeRequest)
        {
            var response = await _incomeService.AddIncome(incomeRequest);
            return Ok(response);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateIncome(IncomeRequestModel incomeRequest)
        {
            await _incomeService.UpdateIncome(incomeRequest);
            return Ok(incomeRequest);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            await _incomeService.DeleteIncome(id);
            return Ok("Delete Successed!");
        }






    }
}
