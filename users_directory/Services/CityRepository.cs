using users_directory.DB;
using users_directory.Models;

namespace users_directory.Services
{
    public class CityRepository:Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
