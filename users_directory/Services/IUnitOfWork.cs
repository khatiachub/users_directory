namespace users_directory.Services
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        Task<bool> CompleteAsync();
    }
}
