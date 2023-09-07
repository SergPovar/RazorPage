using RazorPage.Models;

namespace RazorPage.Services;

public class MokEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;

    public MokEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new Employee()
            {
                Id = 1, Name = "Mary", Email = "mary@mail.ru", PhotoPath = "avatar.png", Department = Dept.HR
            },
            new Employee()
            {
            Id = 2, Name = "Ivan", Email = "ivan@mail.ru", PhotoPath = "avatar2.png", Department = Dept.Payroll
           },
            new Employee()
            {
                Id = 3, Name = "Serg", Email = "serg@mail.ru", PhotoPath = "avatar3.png", Department = Dept.IT
            },
            new Employee()
            {
                Id = 4, Name = "Tora", Email = "tora@mail.ru", PhotoPath = "avatar4.png", Department = Dept.Payroll
            },new Employee()
            {
                Id = 5, Name = "Natasha", Email = "natasha@mail.ru", PhotoPath = "avatar5.png", Department = Dept.HR
            },
            new Employee()
            {
                Id = 6, Name = "Lexa", Email = "lexa@mail.ru",  Department = Dept.IT
            }
        };
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _employeeList;
    }

    public IEnumerable<Employee> SearchEmployees(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return _employeeList;
        }

        return _employeeList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())|| x.Email.ToLower().Contains(searchTerm.ToLower()));
    }

    public Employee GetEmployee(int id)
    {
        return _employeeList.FirstOrDefault(x=>x.Id==id);
    }

    public Employee Update(Employee updatedEmployee)
    {
        Employee employee = _employeeList.FirstOrDefault(x =>x.Id ==updatedEmployee.Id);
        if (employee!=null)
        {
            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;
            employee.PhotoPath = updatedEmployee.PhotoPath;
            employee.Email = updatedEmployee.Email;
        }

        return employee;

    }

    public Employee Add(Employee newEmployee)
    {
        newEmployee.Id = _employeeList.Max(x => x.Id)+1;
        _employeeList.Add(newEmployee);
        return newEmployee;
    }

    public Employee Delete(int id)
    {
        Employee employeeDelete = _employeeList.FirstOrDefault(x => x.Id == id);
        if (employeeDelete!=null)
        {
            _employeeList.Remove(employeeDelete);
        }

        return employeeDelete;
    }

    public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
    {
        IEnumerable<Employee> query = _employeeList;
        if (dept.HasValue)
        {
            query = query.Where(x => x.Department == dept.Value);
        }
        return query.GroupBy(x => x.Department)
            .Select(x => new DeptHeadCount()
            {
                Department = x.Key.Value,
                Count = x.Count()
            }).ToList();
    }
}