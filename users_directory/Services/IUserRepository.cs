using users_directory.DTO;
using users_directory.Models;



namespace users_directory.Services
{
    public interface IUserRepository:IRepository<User>
    {
        Task<(IEnumerable<User>, int)> SearchUsersAsync(UserSearchDto searchFilter, int page, int pageSize);
        Task<IEnumerable<User?>> GetAllAsync();
        Task<IEnumerable<User>> GetByPersonalNumAsync(string firstname, string lastname, string personalnumber);
        Task<User?> GetByIdAsync(int id);
        Task<string> UploadProfileImage(IFormFile profileImage);
        Task<bool> Delete(int id);    
    }
}
