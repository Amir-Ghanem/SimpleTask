using Microsoft.EntityFrameworkCore;
using SimpleTask.Models;

namespace SimpleTask.Repository.User
{
    public class UserRepository:IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

       
        public async Task<Models.User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
