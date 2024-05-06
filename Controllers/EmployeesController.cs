using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTask.Actions;
using SimpleTask.Models;
using SimpleTask.Repository.Department;
using SimpleTask.Repository.Employee;
using SimpleTask.Repository.User;

namespace SimpleTask.Controllers
{
    [AuthenticationActionFilter]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRepository _userRepository;

        public EmployeesController(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _userRepository = userRepository;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString, int? pageNumber = 1)
        {
            ViewData["CurrentFilter"] = searchString;

            int pageSize = 3; 

            var paginationModel = await _employeeRepository.GetAllEmployeesAsync(searchString, pageNumber ?? 1, pageSize);

            return View(paginationModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Users = await _userRepository.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ModifiedByID,CreatedByID,Name,DateOfBirth,Mobile,Email,HireDate,Salary,ProfileImage,File,DepartmentID,CreatedDate,ModifiedDate,CreatedByID,ModifiedByID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeRepository.CreateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Users = await _userRepository.GetAllUsersAsync();
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Users = await _userRepository.GetAllUsersAsync();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ModifiedByID,CreatedByID,Name,DateOfBirth,Mobile,Email,HireDate,Salary,ProfileImage,File,DepartmentID,CreatedDate,ModifiedDate,CreatedByID,ModifiedByID")] Employee employee)
        {
            if (id != employee.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeRepository.UpdateEmployeeAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await _departmentRepository.GetAllDepartmentsAsync();
            ViewBag.Users = await _userRepository.GetAllUsersAsync();
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            var employee = _employeeRepository.GetEmployeeByIdAsync(id).Result;
            return employee != null;
        }
    }
}
