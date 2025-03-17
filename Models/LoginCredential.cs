using System.ComponentModel.DataAnnotations;

namespace Dot_net_ModelViewController.Models
{
    public class LoginCredential
    {
        [Key]
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
