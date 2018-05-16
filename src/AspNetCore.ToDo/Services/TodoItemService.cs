using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ToDo.Models;
using AspNetCore.ToDo.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.ToDo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private ApplicationDbContext _context;
        public TodoItemService(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<bool> AddItemAsync(NewTodoItem newItem)
        {
            var entity=new TodoItem
            {
                Id=Guid.NewGuid(),
                IsDone=false,
                Title=newItem.Title,
                DueAt=DateTimeOffset.Now.AddDays(3)
            };

            _context.Items.Add(entity);

            var saveResult=await _context.SaveChangesAsync();
            return saveResult==1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemAsync(ApplicationUser user)
        {
            var items=await _context.Items.Where(x=>x.IsDone==false&&x.OwnerId==user.Id).ToArrayAsync();
            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item=await _context.Items.Where(x=>x.Id==id).SingleOrDefaultAsync();
            if(item==null)return false;

            item.IsDone=true;
            var saveResult=await _context.SaveChangesAsync();
            return saveResult==1;
        }
    }
}
