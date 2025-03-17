using System.IdentityModel.Tokens.Jwt;

namespace Dot_net_ModelViewController.Interface
{
    public interface IUserAuth
    {
        string Authentication(string username, string password);
    }
}
