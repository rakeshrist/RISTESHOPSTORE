using System;
using System.Security.Cryptography;
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
        public async Task<tbl_user> Authenticate(string username, string passwordText)
        {
            var user = await dc.tbl_users.FirstOrDefaultAsync(x => x.user_name == username);
            if (user == null || user.password_key == null || user.password == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.password, user.password_key))
            {
                return null;
            }
            return user;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] password_key)
        {
            using (var hmac = new HMACSHA512(password_key))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));
                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            tbl_user user = new tbl_user();
            user.user_name = userName;
            user.password = passwordHash;
            user.password_key = passwordKey;
            dc.tbl_users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.tbl_users.AnyAsync(x => x.user_name == userName);
        }
    }
}