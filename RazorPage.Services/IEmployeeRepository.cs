namespace RazorPage.Services;
using RazorPage.Models;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAllEmployees();
    IEnumerable<Employee> SearchEmployees(string searchTerm);
    Employee GetEmployee(int id);
    Employee Update(Employee updatedEmployee);
    Employee Add(Employee newEmployee);
    Employee Delete(int id);
    IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
}