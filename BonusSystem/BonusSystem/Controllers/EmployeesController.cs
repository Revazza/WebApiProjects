using BonusSystem.Db;
using BonusSystem.Db.Entities;
using BonusSystem.Models;
using BonusSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonusSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(
            IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }




        [HttpGet("get-all-employee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeService.GetAllEmployAsync());
        }

        [HttpGet("most-bonused-employees")]
        public async Task<IActionResult> GetTop10MostBonusedEmployee()
        {
            var top10 = await _employeeService.Top10EmployeeWithMostBonusAsync();
            return Ok(top10);
        }

        [HttpGet("most-bonused-recommendators")]
        public async Task<IActionResult> GetTop10MostBonusedRecommendator()
        {
            var top10 = await _employeeService.Top10RecommendatorWithMostBonusAsync();
            return Ok(top10);
        }

        //[HttpGet("get-all-bonuses")]
        //public async Task<IActionResult> GetAllBonusesFromGiveDate()
        //{
        //    return Ok(await _context.Bonuses.ToListAsync());
        //}
        [HttpPost("get-all-bonuses-from-given-date")]
        public async Task<IActionResult> GetAllBonusesFromGiveDate([FromBody] DateTime from)
        {
            return Ok(await _employeeService.GetBonusesFromDateAsync(from));
        }

        [HttpPost("add-bonus-to-employee")]
        public async Task<IActionResult> AddBonusToEmployee(Guid userId, double bonus)
        {

            await _employeeService.GiveBonusAsync(userId, bonus);
            await _employeeService.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {

            return Ok(_employeeService.AddEmployeeAsync(request));
        }




    }
}
