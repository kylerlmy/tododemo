using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.ToDo.Controllers {
    public class TodoController : Controller {
        public IActionResult Index () {
             return View ();
        }

       
    }
}