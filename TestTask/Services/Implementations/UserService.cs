using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser()
        {
            var userWithMostOrders = await (from user in _context.Users
                                            join order in _context.Orders on user.Id equals order.UserId into userOrders
                                            orderby userOrders.Count() descending
                                            select user)
                                     .FirstAsync();

            return userWithMostOrders;
        }

        public async Task<List<User>> GetUsers()
        {
            var usersInactive = await (from user in _context.Users
                                       where (user.Status == Enums.UserStatus.Inactive)
                                       select user)
                                     .ToListAsync();

            return usersInactive;
        }
    }
}
