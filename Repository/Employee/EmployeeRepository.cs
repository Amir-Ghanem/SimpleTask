using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SimpleTask.Models;
using SimpleTask.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleTask.Repository.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeRepository(ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<PaginationModel<Models.Employee>> GetAllEmployeesAsync(string searchString, int pageNumber, int pageSize)
        {
            var query = _context.Employees.Include(e => e.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(e => e.Name.Contains(searchString) || e.Email.Contains(searchString));
            }

            var totalCount = await query.CountAsync();

            var employees = await query
                .OrderBy(e => e.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PaginationModel<Models.Employee>(employees, pageNumber, pageSize, totalCount);
        }

        public async Task<Models.Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.Include(e => e.Department)
                .Include(e => e.CreatedBy)
                .Include(e => e.ModifiedBy)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task CreateEmployeeAsync(Models.Employee employee)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            string DirectoryName = Path.Combine(webRootPath, "uploads/employees");
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }

            if (employee.File != null && employee.File.Length > 0)
            {


                var fileName = employee.File.FileName; // Specify the desired file name

                var imagePath = Path.Combine(DirectoryName, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await employee.File.CopyToAsync(stream);
                }
                employee.ProfileImage = fileName;
            }
            employee.CreatedByID = CurrentUser.Id;
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Models.Employee employee)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            string DirectoryName = Path.Combine(webRootPath, "uploads/employees");
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }

            if (employee.File != null && employee.File.Length > 0)
            {


                var fileName = employee.File.FileName; // Specify the desired file name

                var imagePath = Path.Combine(DirectoryName, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await employee.File.CopyToAsync(stream);
                }
                employee.ProfileImage = fileName;
            }
            employee.ModifiedByID = CurrentUser.Id;
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            var webRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, "uploads/employees", employee.ProfileImage);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
