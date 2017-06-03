using System.Collections.Generic;

namespace Petrovich.Web.Models.UserManagement
{
    public interface IUpdateApplicationUserViewModel
    {
        string Id { get; }
        string Email { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        List<string> Claims { get; set; }
    }
}