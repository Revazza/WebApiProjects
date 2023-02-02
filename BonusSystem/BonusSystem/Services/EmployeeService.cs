using BonusSystem.Db;
using BonusSystem.Db.Entities;
using BonusSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;

namespace BonusSystem.Services
{
    public interface IEmployeeService
    {
        Task<int> GetBonusesFromDateAsync(DateTime from);
        Task<List<MostBonusEmployee>> Top10EmployeeWithMostBonusAsync();
        Task GiveBonusAsync(Guid employeeId, double bonus);
        Task<List<MostBonusEmployee>> Top10RecommendatorWithMostBonusAsync();
        Task<EmployeeEntity> AddEmployeeAsync(AddEmployeeRequest request);
        Task<List<EmployeeEntity>> GetAllEmployAsync();
        Task SaveChangesAsync();


    }

    public class EmployeeService : IEmployeeService
    {
        private readonly BonusSystemDbContext _context;

        public EmployeeService(BonusSystemDbContext context)
        {
            _context = context;
        }



        private async Task<List<BonusEntity>> GenerateBonusForEmployeesAsync(
            Guid employeeId,
            double bonus)
        {
            var bonuses = new List<BonusEntity>();


            for (int i = 0; i < 3; i++)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    break;
                }

                var newBonus = new BonusEntity()
                {
                    EmployeeId = employee.Id,
                    Amount = bonus,
                    CreatedAt = DateTime.UtcNow,
                };

                // when calculating top 10 recommendator with most bonus
                // we're calculating bonus depending on RecommendatorId
                // so last RecommendatorId should be ommited because
                // we need to stop the chain somewhere
                if (employee.RecommendatorId != Guid.Empty && i != 2)
                {
                    newBonus.RecommendatorId = employee.RecommendatorId;
                }


                bonus /= 2;
                employeeId = employee.RecommendatorId;

                bonuses.Add(newBonus);


            }

            return bonuses;
        }

        public async Task GiveBonusAsync(Guid employeeId, double bonus)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                //for demo purposes
                throw new Exception("employee id is not correct");
            }
            if (bonus < employee.Salary / 2 || bonus > employee.Salary * 3)
            {
                throw new Exception("Bonus is depend on salary, invalid bonus");
            }

            var bonuses = await GenerateBonusForEmployeesAsync(employeeId, bonus);

            await _context.Bonuses.AddRangeAsync(bonuses);

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetBonusesFromDateAsync(DateTime from)
        {
            var bonuses = await _context.Bonuses.Where(b => b.CreatedAt > from).ToListAsync();
            return bonuses.Count;
        }


        private async Task<List<MostBonusEmployee>> GetEmployeesWithMostBonusAsync(
            Dictionary<Guid, double> dictionary
            )
        {
            var employees = _context.Employees;
            dictionary = dictionary
                .OrderByDescending(d => d.Value)
                .Take(10)
                .ToDictionary(d => d.Key, d => d.Value);

            var list = new List<MostBonusEmployee>();

            foreach (var item in dictionary)
            {
                var employee = await employees.FirstOrDefaultAsync(e => e.Id == item.Key);

                if (employee != null)
                {
                    var mostBonusEmployee = new MostBonusEmployee()
                    {
                        Id = employee.Id,
                        Amount = item.Value,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                    };
                    list.Add(mostBonusEmployee);
                }
            }

            return list;
        }

        public async Task<List<MostBonusEmployee>> Top10EmployeeWithMostBonusAsync()
        {
            var bonuses = _context.Bonuses;
            var dictionary = new Dictionary<Guid, double>();

            await bonuses.ForEachAsync(b =>
            {
                if (dictionary.ContainsKey(b.EmployeeId))
                {
                    dictionary[b.EmployeeId] += b.Amount;
                }
                else
                {
                    dictionary.Add(b.EmployeeId, b.Amount);
                }
            });

            var mostBonusedEmployees = await GetEmployeesWithMostBonusAsync(dictionary);

            return mostBonusedEmployees;
        }

        public async Task<List<MostBonusEmployee>> Top10RecommendatorWithMostBonusAsync()
        {
            var bonuses = _context.Bonuses;
            var dictionary = new Dictionary<Guid, double>();

            await bonuses.ForEachAsync(b =>
            {

                if (dictionary.ContainsKey(b.RecommendatorId))
                {
                    dictionary[b.RecommendatorId] += b.Amount / 2;
                }
                else
                {
                    dictionary.Add(b.RecommendatorId, b.Amount / 2);
                }
            });

            var top10 = await GetEmployeesWithMostBonusAsync(dictionary);

            return top10;
        }

        public async Task<EmployeeEntity> AddEmployeeAsync(AddEmployeeRequest request)
        {
            var recommendator = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.RecommendatorId);

            var newEmployee = new EmployeeEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PersonalNumber = request.PersonalNumber,
                Salary = request.Salary,
            };

            if (recommendator != null)
            {
                newEmployee.RecommendatorId = recommendator.Id;
            }


            await _context.Employees.AddAsync(newEmployee);

            await _context.SaveChangesAsync();

            return newEmployee;
        }

        public async Task<List<EmployeeEntity>> GetAllEmployAsync()
        {
            return await _context.Employees.ToListAsync();
        }
    }
}
