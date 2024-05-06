using SimpleTask.Models.ViewModels;

namespace SimpleTask.Repository.Employee
{
    public interface IEmployeeRepository
    {
        Task<PaginationModel<Models.Employee>> GetAllEmployeesAsync(string searchString,int pageNumber,int pageSize);
        Task<Models.Employee> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(Models.Employee employee);
        Task UpdateEmployeeAsync(Models.Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
