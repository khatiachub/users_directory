using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using users_directory.DB;
using users_directory.Models;

namespace users_directory.Services
{
    public class RelatedPersonRepository : Repository<PersonRelationship>,IRelatedPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public RelatedPersonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task DeleteRelated(int id, int userId)
        {
         var entity = await _context.PersonRelationships
        .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

         if (entity == null)
         {
        throw new KeyNotFoundException($"Entity with ID {id} and User ID {userId} not found.");
         }

         _context.PersonRelationships.Remove(entity);
          await _context.SaveChangesAsync();
        }

    }
}
