using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //needed for IdentityDbContext
using Microsoft.EntityFrameworkCore;

namespace WebAPISandwich.Model
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }
    }
}
