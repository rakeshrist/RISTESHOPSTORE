namespace WebAPI.Interfaces
{
    using System.Threading.Tasks;
    using WebAPI.Models;
    public interface IUserRepository
    {
        Task<tbl_user> Authenticate(string username, string password);
        void Register(string userName, string password);
        Task<bool> UserAlreadyExists(string userName);
    }
}