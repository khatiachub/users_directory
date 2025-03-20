using Microsoft.EntityFrameworkCore;
using users_directory.DB;
using users_directory.Models;

namespace users_directory.Services
{
    public class PhoneNumbersRepository:Repository<PhoneNumber>,IPhoneNumbersRepository
    {
        private readonly ApplicationDbContext _context;

        public PhoneNumbersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
      
       
    }
}
