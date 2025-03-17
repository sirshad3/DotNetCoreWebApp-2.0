using Dot_net_ModelViewController.Models;

namespace Dot_net_ModelViewController.Interface
{
    public interface IUser
    {
        List<User> GetAllUsers();
        Task<User> GetUser(int UserId);
        Task<IEnumerable<User>> SearchUser(string name, string userEmail);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(int UserId);
    }
}
