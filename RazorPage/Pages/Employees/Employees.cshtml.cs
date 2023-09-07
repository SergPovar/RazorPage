using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;
using RazorPage.Services;

namespace RazorPage.Pages.Employees;

public class Employees : PageModel
{
    private readonly IEmployeeRepository _db;
    public IEnumerable<Employee> Employee { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }
    public Employees(IEmployeeRepository db)
    {
        _db = db;
    }
  
    public void OnGet()
    {
        Employee = _db.SearchEmployees(SearchTerm);
    }
}