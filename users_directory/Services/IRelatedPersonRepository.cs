using users_directory.Models;

namespace users_directory.Services
{
    public interface IRelatedPersonRepository:IRepository<PersonRelationship>
    {
        Task DeleteRelated(int id, int userId);
    }
}
