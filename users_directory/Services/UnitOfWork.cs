using System;
using users_directory.DB;
using users_directory.Models;

namespace users_directory.Services
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
       // public IRepository<PhoneNumber> PhoneNumbers { get; }
       // public IRepository<PersonRelationship> Relationships { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
           // PhoneNumbers = new Repository<PhoneNumber>(context);
           // Relationships = new Repository<PersonRelationship>(context);
        }

        public async Task<bool> CompleteAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
