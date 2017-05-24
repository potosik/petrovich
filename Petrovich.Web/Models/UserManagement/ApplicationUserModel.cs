using Petrovich.Web.Core.Security.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Petrovich.Web.Models.UserManagement
{
    public class ApplicationUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        public static ApplicationUserModel Create(ApplicationUser user)
        {
            return new ApplicationUserModel()
            {
                Id = user.Id,
                Email = user.Email,
            };
        }
    }
}