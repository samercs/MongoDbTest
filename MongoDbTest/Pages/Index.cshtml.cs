using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDbTest.Models;
using MongoDbTest.Services;

namespace MongoDbTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EmployeeService _employeeService;
        public IReadOnlyCollection<Employee> Data { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }
        public IndexModel(ILogger<IndexModel> logger, EmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task OnGetAsync()
        {
            Data = await _employeeService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Employee.Id))
                {
                    await _employeeService.Create(Employee);
                }
                else
                {
                    await _employeeService.Update(Employee);
                }
                
            }
            return RedirectToPage(nameof(Index));
        }

        public async Task<IActionResult> OnGetDelete(string id)
        {
            await _employeeService.Delete(id);
            return RedirectToPage(nameof(Index));
        }

        public async Task OnGetEdit(string id)
        {
            Employee = await _employeeService.GetById(id);
            Data = await _employeeService.GetAll();
        }
    }
}
