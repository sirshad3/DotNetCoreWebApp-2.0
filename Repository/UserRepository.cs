using Dot_net_ModelViewController.Data;
using Dot_net_ModelViewController.Interface;
using Dot_net_ModelViewController.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot_net_ModelViewController.Repository
{
    public class UserRepository : IUser
    {
        private readonly AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public List<User> GetAllUsers()
        {
            return appDbContext.User.ToList();
        }

        public async Task<User> GetUser(int UserId)
        {
            return await appDbContext.User
                .FirstOrDefaultAsync(u => u.Id == UserId);
        }
        public async Task<IEnumerable<User>> SearchUser(string name, string userEmail)
        {
            return await appDbContext.User.Where(u => u.Name == name.Trim() || u.Email == userEmail).ToListAsync();
            //return await appDbContext.User
            //    .FirstOrDefaultAsync(u => u.Name == name.Trim());
        }

        public async Task<User> AddUser(User user)
        {
            var result = await appDbContext.User.AddAsync(user);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> UpdateUser(User user)
        {
            var result = await appDbContext.User
                .FirstOrDefaultAsync(e => e.Id == user.Id);

            if (result != null)
            {
                result.Name = user.Name;
                result.UserName = user.UserName;
                result.Email = user.Email;
                result.Password = user.Password;
                result.Gender = user.Gender;
                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var result = await appDbContext.User
                .FirstOrDefaultAsync(e => e.Id == userId);
            if (result != null)
            {
                appDbContext.User.Remove(result);
                await appDbContext.SaveChangesAsync();
            }
            return result;
        }
    }
}
