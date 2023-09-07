using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;
using RazorPage.Services;

namespace RazorPage.Pages.Employees;

public class Delete : PageModel
{
    [BindProperty] public Employee Employee { get; set; }

    private readonly IEmployeeRepository _employeeRepository;
    private IWebHostEnvironment _webHostEnvironment;


    public Delete(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
    {
        _employeeRepository = employeeRepository;
        _webHostEnvironment = webHostEnvironment;
    }


    public IActionResult OnGet(int id)
    {
        Employee = _employeeRepository.GetEmployee(id);
        if (Employee == null)
        {
            return RedirectToPage("/NotFound");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        var deletetedEmployee = _employeeRepository.Delete(Employee.Id);

        if (deletetedEmployee.PhotoPath != null)
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", deletetedEmployee.PhotoPath);
            if (deletetedEmployee.PhotoPath != "noimage.png")
            {
                System.IO.File.Delete(filePath);
            }
        }

        if (deletetedEmployee == null)
        {
            return RedirectToPage("/NotFound");
        }

        return RedirectToPage("Employees");
    }
}