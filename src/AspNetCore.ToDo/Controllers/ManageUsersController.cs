using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AspNetCore.ToDo.Models.TodoViewModels;

namespace AspNetCore.ToDo.Controllers
{
    public class ManageUsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins=await _userManager.GetUsersInRoleAsync("Administrator");
            var everyone=await _userManager.Users.ToArrayAsync();

            var model=new ManagerUsersViewModel{
                Administrator=admins,
                EveryOne=everyone
            };

            return View(model);
        }
    }
}