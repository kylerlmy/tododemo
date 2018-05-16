using System;
using System.Collections.Generic;

namespace AspNetCore.ToDo.Models
{
    public class ManageUsersViewModel
    {
        public IEnumerable<ApplicationUser> Administrators { get; set; }
        public IEnumerable<ApplicationUser> Everyone { get; set; }
    }
}