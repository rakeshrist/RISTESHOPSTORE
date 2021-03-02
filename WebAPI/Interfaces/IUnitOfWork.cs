namespace WebAPI.Interfaces
{
    using System.Threading.Tasks;
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get; }
        IUserRepository userRepository { get; }
        Task<bool> SaveAsync();
    }
}