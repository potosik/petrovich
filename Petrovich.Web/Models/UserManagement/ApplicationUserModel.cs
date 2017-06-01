using Petrovich.Web.Core.Extensions;
using Petrovich.Web.Core.Security.DbContext.Entities;

namespace Petrovich.Web.Models.UserManagement
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public static ApplicationUserModel Create(ApplicationUser user)
        {
            return new ApplicationUserModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.Claims.GetUserName(),
            };
        }
    }
}