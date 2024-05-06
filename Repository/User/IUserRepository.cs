namespace SimpleTask.Repository.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<Models.User>> GetAllUsersAsync();
        Task<Models.User> GetUserByEmailAsync(string email);
    }
}
