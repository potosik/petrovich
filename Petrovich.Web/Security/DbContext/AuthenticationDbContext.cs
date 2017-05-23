using Microsoft.AspNet.Identity.EntityFramework;
using Petrovich.Web.Security.DbContext.Entities;

namespace Petrovich.Web.Security.DbContext
{
    public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthenticationDbContext()
            : base("authDatabaseConnectionString", throwIfV1Schema: false)
        {
        }

        public static AuthenticationDbContext Create()
        {
            return new AuthenticationDbContext();
        }
    }
}