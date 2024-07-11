using System.ComponentModel.DataAnnotations;

namespace CoreAPI.Models
{
    public class UserModel
    {
        public int? Id { get; set; } = null;
        public string Username { get; set; } = null;
        public string Password { get; set; } = null;
        public int? Rol { get; set; } = null;
    }
}
