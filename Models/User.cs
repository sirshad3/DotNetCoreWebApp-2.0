using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dot_net_ModelViewController.Models
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string? UserName { set; get; }
        [Required]
        public string? Name { set; get; }
        [Required]
        public string? Password { set; get; }
        [Required]
        public string? Email { set; get; }
        public char? Gender { set; get; }
    }
}
