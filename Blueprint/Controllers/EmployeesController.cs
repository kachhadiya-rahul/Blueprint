using Blueprint.Dtos;
using Blueprint.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blueprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ApplicationContext _context;
        public EmployeesController(ApplicationContext context)
        {
            _context = context;
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employees.OrderByDescending(it => it.Id).Select(e=>
            new {
                e.Id,
                e.FullName,
                e.Address,
                e.PhoneNumber,
                e.Position
            }).ToList();

            return Ok(employees);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            if (employee == null) return BadRequest("Please provide inputs");

            var position = await _context.EmployeePositions.FindAsync(employee.position);
            if (position == null) return NotFound("Position not found");

            var newEmployee = new Employee
            {
                FullName = employee.FullName,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Position = position
            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            return Get();
            

        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EmployeeDto employee)
        {
            if (employee == null || employee.Id == 0) return BadRequest("Please provide inputs");

            var employeeToBeUpdated = await _context.Employees.FindAsync(employee.Id);
            if (employeeToBeUpdated == null) return NotFound("No such employee found");
            EmployeePosition position = null;
           

            if (employee.Address != null) employeeToBeUpdated.Address = employee.Address;
            if (employee.FullName != null) employeeToBeUpdated.FullName = employee.FullName;
            if (employee.PhoneNumber != null) employeeToBeUpdated.PhoneNumber = employee.PhoneNumber;
            if(employee.position != null)
            {
                position = await _context.EmployeePositions.FindAsync(employee.position);
                employeeToBeUpdated.Position = position;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                employeeToBeUpdated.FullName,
                employeeToBeUpdated.Address,
                employeeToBeUpdated.PhoneNumber,
                employeeToBeUpdated.Position
            });
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeToBeDeteted = _context.Employees.Find(id);
            if (employeeToBeDeteted != null)
            {
                _context.Employees.Remove(employeeToBeDeteted);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
