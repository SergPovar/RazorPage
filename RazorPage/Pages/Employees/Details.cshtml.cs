using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;
using RazorPage.Services;

namespace RazorPage.Pages.Employees;

public class Details : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    public Employee Employee { get; set; }
    
    public Details(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    public IActionResult OnGet(int ID)
    {
        Employee = _employeeRepository.GetEmployee(ID);
        if (Employee==null)
        {
            return RedirectToPage("/NotFound");
        }

        return Page();
    }
}