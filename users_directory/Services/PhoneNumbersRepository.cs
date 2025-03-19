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
      
        public async Task<bool> Delete(int id)
        {
            var entity = await _context.PhoneNumbers.FirstOrDefaultAsync(e => e.PersonId == id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return true;
        }
    }
}
