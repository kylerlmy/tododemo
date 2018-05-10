using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Models;
using AspNetCore.ToDo.Models.TodoViewModels;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.ToDo.Services;

namespace AspNetCore.ToDo.Controllers {
    public class TodoController : Controller {

        private readonly ITodoItemService _todoItemService;
        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService=todoItemService;
        }

       public async Task<IActionResult> Index()
       {
           var todoItems=await _todoItemService.GetIncompleteItemAsync();
           var model=new TodoViewModel()
           {
               Items=todoItems
           };
           return View(model);
       }
    }
}