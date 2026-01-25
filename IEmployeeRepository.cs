using AddSolBackend;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeRepository
{
    // The "Menu" of what our data layer can do
    Task<IEnumerable<Employee>> GetAllEmployees();

    // FIX: Added '?' to allow returning null if ID is not found
    Task<Employee?> GetEmployeeById(int id);

    Task AddEmployee(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task DeleteEmployee(int id);
}
