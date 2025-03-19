using System;
using users_directory.DB;
using users_directory.Models;

namespace users_directory.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IRelatedPersonRepository Relationships { get; }
        public ICityRepository City {get;}
        public IPhoneNumbersRepository PhoneNumbers { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Relationships = new RelatedPersonRepository(_context);
            City=new CityRepository(_context);
            PhoneNumbers = new PhoneNumbersRepository(_context);
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
