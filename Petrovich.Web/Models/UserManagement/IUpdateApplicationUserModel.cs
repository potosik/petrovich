using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.UserManagement
{
    public interface IUpdateApplicationUserModel
    {
        string Id { get; }
        string Email { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        List<string> Claims { get; set; }
    }
}