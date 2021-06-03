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
    public class EmployeePositionsController : ControllerBase
    {
        private ApplicationContext _context;

        public EmployeePositionsController(ApplicationContext context)
        {
            this._context = context;
        }

        // GET: api/<EmployeePositionsController>
        [HttpGet]
        public  IActionResult Get()
        {
            return Ok( _context.EmployeePositions.Select(p => p));
        }

        

        // POST api/<EmployeePositionsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeePositionDto position)
        {
            var newPosition = new EmployeePosition { PositionName = position.Name };
            _context.EmployeePositions.Add(newPosition);
            await _context.SaveChangesAsync();

            return Get();
        }

    }
}
