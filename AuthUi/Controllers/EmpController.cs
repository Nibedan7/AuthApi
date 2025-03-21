using Microsoft.AspNetCore.Mvc;
using AuthUi.Models;  // Assuming Employee model is here
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace AuthUi.Controllers
{
    public class EmpController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7259";
        private readonly EmployeeApiHelper _employeeApiHelper;

        public EmpController(IHttpContextAccessor httpContextAccessor)
        {

            var jwtToken = httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            _employeeApiHelper = new EmployeeApiHelper(_baseUrl, jwtToken);
        }

        // Action to show all employees
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToAction("Login", "UserAuthMvc");
            }

            var employeesJson = await _employeeApiHelper.GetAllEmployeesAsync();

            if (string.IsNullOrEmpty(employeesJson) ||
                (!employeesJson.StartsWith("[") && !employeesJson.StartsWith("{")))
            {
                Console.WriteLine($"Invalid response from API: {employeesJson}");
                return View("Error", new ErrorViewModel { RequestId = "Invalid API Response" });
            }

            try
            {
                var employees = JsonConvert.DeserializeObject<List<Employee>>(employeesJson);
                return View(employees);
            }
            catch (JsonReaderException jEx)
            {
                // Handle JSON deserialization error
                Console.WriteLine($"JSON Parsing Error: {jEx.Message}");
                return View("Error", new ErrorViewModel { RequestId = "Error parsing employee data." });
            }
        }

        public IActionResult Add()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToAction("Login", "UserAuthMvc");
            }
            return View();
        }

        // Action to handle adding an employee
        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeApiHelper.AddEmployeeAsync(employee);

                return RedirectToAction("Index");
            }

            return View(employee);
        }



        // Action to show the employee update form
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToAction("Login", "UserAuthMvc");
            }
            var employeesJson = await _employeeApiHelper.GetEmployeeByIdAsync(id);
            var employee = JsonConvert.DeserializeObject<Employee>(employeesJson);

            if (employee == null)
            {
                return NotFound();  // Employee not found
            }

            return View(employee);
        }

        // Action to handle updating an employee
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var response = await _employeeApiHelper.UpdateEmployeeAsync(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // Action to display employee details (GET method)
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (token == null)
            {
                return RedirectToAction("Login", "UserAuthMvc");
            }

            var employeesJson = await _employeeApiHelper.GetEmployeeByIdAsync(id);
            var employee = JsonConvert.DeserializeObject<Employee>(employeesJson);

            if (employee == null)
            {
                return NotFound();  // Employee not found
            }

            return View(employee);  
        }

        // Action to delete an employee (POST method)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> PostDelete(int id)
        {
            var response = await _employeeApiHelper.DeleteEmployeeAsync(id);
            return RedirectToAction("Index");  
        }
    }
}