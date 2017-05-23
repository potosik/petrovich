using Microsoft.AspNet.Identity.EntityFramework;
using Petrovich.Web.Core.Security.DbContext.Entities;

namespace Petrovich.Web.Core.Security.DbContext
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