namespace users_directory.Services
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        IRelatedPersonRepository Relationships { get; }
        IPhoneNumbersRepository PhoneNumbers { get; }
        ICityRepository City {  get; }
        Task<bool> CompleteAsync();
    }
}
