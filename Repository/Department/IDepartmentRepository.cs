namespace SimpleTask.Repository.Department
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Models.Department>> GetAllDepartmentsAsync();
        Task<Models.Department> GetDepartmentByIdAsync(int id);
    }
}
