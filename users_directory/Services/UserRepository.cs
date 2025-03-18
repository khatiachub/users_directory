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
                .Include(u => u.PhoneNumbers)
                .Include(u => u.City)
                .Include(u => u.Relationships)
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
        public async Task<(IEnumerable<User>, int)> SearchUsersAsync(UserGetDto? searchFilter, int page, int pageSize)
        {
            var query = _context.People
                            .Include(u => u.PhoneNumbers)
                            .Include(u => u.City)
                            .Include(u => u.Relationships)
                            .AsQueryable();

            if (searchFilter != null)
            {
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
                if (searchFilter.PhoneNumbers != null && searchFilter.PhoneNumbers.Any())
                {
                    var phoneNumbersToSearch = searchFilter.PhoneNumbers.Select(p => p.Number).ToList();
                    query = query.Where(u => u.PhoneNumbers.Any(p => phoneNumbersToSearch.Contains(p.Number)));
                }
                if (searchFilter.Gender != null)
                {
                    query = query.Where(u => u.Gender == searchFilter.Gender);
                }
                if (searchFilter.Relationships!= null && searchFilter.Relationships.Any())
                {
                    var relatedToSearch = searchFilter.Relationships.Select(p => p.RelatedPerson).ToList();
                    query = query.Where(u => u.Relationships.Any(p => relatedToSearch.Contains(p.RelatedPerson)));
                }

            }

            int totalCount = await query.CountAsync();
            var users = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return (users, totalCount);
        }
        
        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.People
                .Include(u => u.PhoneNumbers)  
                .Include(u => u.City)
                .Include(u => u.Relationships)
                .ToListAsync();
        }
        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _context.People
                            .Include(u => u.PhoneNumbers)
                            .Include(u => u.City)
                            .Include(u => u.Relationships)
                            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<string> UploadProfileImage(IFormFile profileImage)
        {
            string profileImageFilename = "";
            try
            {
                if (profileImage != null)
                {
                    var profileImageExtension = "." + profileImage.FileName.Split('.')[profileImage.FileName.Split('.').Length - 1];
                    profileImageFilename = DateTime.Now.Ticks.ToString() + profileImageExtension;

                    var profileImageFilepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                    if (!Directory.Exists(profileImageFilepath))
                    {
                        Directory.CreateDirectory(profileImageFilepath);
                    }

                    var profileImageExactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", profileImageFilename);
                    using (var profileImageStream = new FileStream(profileImageExactpath, FileMode.Create))
                    {
                        await profileImage.CopyToAsync(profileImageStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return (profileImageFilename);
        }

    }
}
