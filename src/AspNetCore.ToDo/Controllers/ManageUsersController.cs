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
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.ToDo.Controllers
{
     [Authorize(Roles = "Administrator")]//如果没有这个特性，表示，普通用户也可以进入用户管理界面
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

            var model=new ManageUsersViewModel{
                Administrators=admins,
                Everyone=everyone
            };

            return View(model);
        }
    }
}