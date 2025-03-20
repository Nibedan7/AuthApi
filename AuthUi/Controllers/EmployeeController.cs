using AuthUi.Models;
using AuthUi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace AuthUi.Controllers {

    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        public IActionResult EditEmployee(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Employee? emp = _db.Employees.Find(id);

            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        public IActionResult DeleteEmployee(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Employee? emp = _db.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);

        }
    }
}
