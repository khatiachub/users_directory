using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using users_directory.DB;
using users_directory.DTO;
using users_directory.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace users_directory.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetByPersonalNumAsync(string firstname, string lastname, string personalnumber)
        {
            var query = _context.People
                 .Include(u => u.City)
                .Include(u => u.PhoneNumbers)  
                .ThenInclude(p => p.NumberType)
                .Include(u => u.Relationships)
                .ThenInclude(p => p.RelatedType)
                .Include(u => u.Gender)

                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(firstname))
            {
                query = query.Where(u => u.FirstName.Contains(firstname));
            }
            if (!string.IsNullOrWhiteSpace(lastname))
            {
                query = query.Where(u => u.LastName.Contains(lastname));
            }
            if (!string.IsNullOrWhiteSpace(personalnumber))
            {
                query = query.Where(u => u.PersonalNumber.Contains(personalnumber));
            }

            return await query.ToListAsync();
        }
        public async Task<(IEnumerable<User>, int)> SearchUsersAsync(UserSearchDto? searchFilter, int page, int pageSize)
        {
            var query = _context.People
                            .Include(u => u.City)
                .Include(u => u.PhoneNumbers)
                .ThenInclude(p => p.NumberType)
                .Include(u => u.Relationships)
                .ThenInclude(p => p.RelatedType)
                .Include(u => u.Gender)
                            .AsQueryable();

            if (searchFilter != null)
            {
                if (searchFilter.Id!=null)
                    query = query.Where(u => u.Id==searchFilter.Id);
                if (!string.IsNullOrWhiteSpace(searchFilter.FirstName))
                    query = query.Where(u => u.FirstName.Contains(searchFilter.FirstName));

                if (!string.IsNullOrWhiteSpace(searchFilter.LastName))
                    query = query.Where(u => u.LastName.Contains(searchFilter.LastName));

                if (!string.IsNullOrWhiteSpace(searchFilter.PersonalNumber))
                    query = query.Where(u => u.PersonalNumber.Contains(searchFilter.PersonalNumber));

                if (searchFilter.BirthDate.HasValue && searchFilter.BirthDate != DateOnly.MinValue)
                    query = query.Where(u => u.BirthDate == searchFilter.BirthDate);

                if (!string.IsNullOrWhiteSpace(searchFilter.City))
                    query = query.Where(u => u.City.CityName.Contains(searchFilter.City));

                if (!string.IsNullOrWhiteSpace(searchFilter.PhoneNumber))
                {
                    query = query.Where(u => u.PhoneNumbers.Any(p => searchFilter.PhoneNumber.Contains(p.Number)));
                }
                if (searchFilter?.Gender != null)
                {
                    query = query.Where(u => u.Gender.Gender==searchFilter.Gender);
                }
                if (!string.IsNullOrWhiteSpace(searchFilter?.RelatedPerson))
                {
                    query = query.Where(u => u.Relationships.Any(p => searchFilter.RelatedPerson.Contains(p.RelatedPerson)));
                }

            }

            int totalCount = await query.CountAsync();
            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (users, totalCount);
        }
        
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.People
                 .Include(u => u.City)
                .Include(u => u.PhoneNumbers)
                .ThenInclude(p => p.NumberType)
                .Include(u => u.Relationships)
                .ThenInclude(p => p.RelatedType)
                .Include(u => u.Gender)
                .ToListAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await _context.People.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _context.People
                             .Include(u => u.City)
                .Include(u => u.PhoneNumbers)
                .ThenInclude(p => p.NumberType)
                .Include(u => u.Relationships)
                .ThenInclude(p => p.RelatedType)
                .Include(u => u.Gender)
                            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> UploadProfileImage(IFormFile file)
        {
            string savedPath = string.Empty;
            string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Upload", "Files");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            try
            {
                string fileName = $"{Guid.NewGuid()}_{file.FileName}";
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                savedPath = Path.Combine("Upload", "Files", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image: {ex.Message}");
            }

            return savedPath;
        }
        public async Task<IEnumerable<PersonReportDto>> ReportRelatedPersons(int id)
        {
            var report = await _context.PersonRelationships
                .Where(r => r.UserId == id)
                .Join(_context.People,
                    r => r.UserId,
                    u => u.Id,
                    (r, u) => new { u.FirstName, u.LastName, r.RelatedType })
                .GroupBy(x => new { x.FirstName, x.LastName, x.RelatedType.Type }) 
                .Select(g => new PersonReportDto
                {
                    FirstName = g.Key.FirstName,
                    LastName = g.Key.LastName,
                    Type = g.Key.Type, 
                    RelatedPersons = g.Count()
                })
                .ToListAsync();

            if (report == null || !report.Any())
            {
                throw new KeyNotFoundException($"Entity with ID {id} not found.");
            }

            return report;
        }


    }
}
