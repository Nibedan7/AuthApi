using AuthApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq; 
using Microsoft.AspNetCore.Authorization;

namespace AuthApi.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
   // [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AllEmployees()
        {
            var employees = _context.Employees.ToList();

            if (!employees.Any())
            {
                return NotFound("No employees found");
            }

            return Ok(employees);
        }



        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Models  .Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid Employee details");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(AllEmployees), new { id = employee.Id }, employee); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Models.Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid Employee details");
            }

            var existingEmployee = _context.Employees.Find(employee.Id);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Designation = employee.Designation;
            existingEmployee.Department = employee.Department;
            existingEmployee.Joining_date = employee.Joining_date;

            _context.SaveChanges();
           // return Ok(existingEmployee);
            return Ok(new { success = true, message = "Employee updated successfully", employee = existingEmployee });

        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }


        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            if (employeeId <= 0)
            {
                return BadRequest("Invalid Employee ID");
            }

            var existingEmployee = _context.Employees.Find(employeeId);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }

            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
            return Ok(new { success = true, message = "Employee Deleted successfully" });
        }

    }
}
