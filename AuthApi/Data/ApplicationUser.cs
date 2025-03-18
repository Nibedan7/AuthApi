using Microsoft.AspNetCore.Identity;

namespace AuthApi.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
