using users_directory.DB;
using users_directory.DTO;
using users_directory.Models;

namespace users_directory.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
