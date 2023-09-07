using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;
using RazorPage.Services;

namespace RazorPage.Pages.Employees;

public class Edit : PageModel
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    [BindProperty] public Employee Employee { get; set; }
    [BindProperty] public IFormFile? Photo { get; set; }
    [BindProperty] public bool Notify { get; set; }

    public string Massage { get; set; }

    public Edit(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment)
    {
        _employeeRepository = employeeRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult OnGet(int? id)
    {
        if (id.HasValue)
        {
            Employee = _employeeRepository.GetEmployee(id.Value);
        }
        else
        {
            Employee = new Employee();
        }

        if (Employee == null)
        {
            return RedirectToPage("/NotFound");
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            if (Photo != null)
            {
                if (Employee.PhotoPath != null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Employee.PhotoPath);
                    if (Employee.PhotoPath!="noimage.png")
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                Employee.PhotoPath = ProcessUploadFile();
            }

            if (Employee.Id>1)
            {
                Employee = _employeeRepository.Update(Employee);
                TempData["SM"] = $"Update {Employee.Name} successful";
            }
            else
            {
                Employee = _employeeRepository.Add(Employee);
                TempData["SM"] = $"Adding {Employee.Name} successful";
            }
           
            return RedirectToPage("Employees");
        }

        return Page();
    }

    public void OnPostUpdateNotificationPreferences(int id)
    {
        if (Notify)
        {
            Massage = "Thank You for turning on notifications";
        }
        else
        {
            Massage = "You have turned off email notifications";
        }

        Employee = _employeeRepository.GetEmployee(id);
    }

    private string ProcessUploadFile()
    {
        string uniqueFileName = null;
        if (Photo != null)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                Photo.CopyTo(fs);
            }
        }

        return uniqueFileName;
    }
}