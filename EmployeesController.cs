using Microsoft.AspNetCore.Mvc;
using AddSolBackend; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddSolBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // 1. Define the "Contract" (Interface)
        // We do NOT use AppDbContext here anymore.
        private readonly IEmployeeRepository _repository;

        // 2. Dependency Injection
        // The app automatically injects the correct repository here.
        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _repository.GetAllEmployees();
            return Ok(employees);
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _repository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await _repository.AddEmployee(employee);
            
            // Returns the newly created item
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch");
            }

            try 
            {
                await _repository.UpdateEmployee(employee);
            }
            catch
            {
                // Double check if it exists before throwing error
                var existing = await _repository.GetEmployeeById(id);
                if (existing == null) return NotFound();
                else throw;
            }

            return NoContent(); // 204 Success
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _repository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _repository.DeleteEmployee(id);
            return NoContent();
        }
    }
}
