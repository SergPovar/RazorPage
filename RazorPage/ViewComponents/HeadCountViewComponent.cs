using Microsoft.AspNetCore.Mvc;
using RazorPage.Models;
using RazorPage.Services;

namespace RazorPage.ViewComponents;

public class HeadCountViewComponent : ViewComponent
{
    private readonly IEmployeeRepository _employeeRepository;
    public HeadCountViewComponent(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IViewComponentResult Invoke(Dept? dept = null)
    {
        var result = _employeeRepository.EmployeeCountByDept(dept);
        
        return View(result);
    }
}