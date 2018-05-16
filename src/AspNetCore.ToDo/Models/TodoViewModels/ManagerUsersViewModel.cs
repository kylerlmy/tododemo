using System;
using System.Collections.Generic;

namespace AspNetCore.ToDo.Models
{
    public class ManagerUsersViewModel
    {
        public IEnumerable<ApplicationUser> Administrator { get; set; }
        public IEnumerable<ApplicationUser> EveryOne { get; set; }
    }
}