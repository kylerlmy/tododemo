using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Models;

namespace AspNetCore.ToDo.Services
{
    public interface ITodoItemService
    {
     Task<IEnumerable<TodoItem>> GetIncompleteItemAsync();
    }
}
