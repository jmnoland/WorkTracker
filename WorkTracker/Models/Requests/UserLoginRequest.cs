using System.ComponentModel.DataAnnotations;

namespace WorkTracker.Models.Requests
{
    public class UserLoginRequest
    {
        [MaxLength(50)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
