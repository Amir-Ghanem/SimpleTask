using Microsoft.EntityFrameworkCore;
using SimpleTask.Models;

namespace SimpleTask.Repository.Department
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Models.Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }
    }
}
