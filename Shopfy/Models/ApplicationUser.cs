using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Shopfy.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
