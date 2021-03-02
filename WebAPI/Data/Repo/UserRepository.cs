using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreDataContext dc;
        public UserRepository(StoreDataContext dc)
        {
            this.dc = dc;

        }
        public async Task<tbl_user> Authenticate(string username, string password)
        {
            return await dc.tbl_users.FirstOrDefaultAsync(x => x.user_name == username && x.password == password);
        }
    }
}