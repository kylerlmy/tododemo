﻿using System;
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
            _context = context;
        }

        public async Task<bool> AddItemAsync(NewTodoItem newItem, ApplicationUser user)
        {
            var entity = new TodoItem
            {
                Id = Guid.NewGuid(),
                OwnerId = user.Id,
                IsDone = false,
                Title = newItem.Title,
                DueAt = DateTimeOffset.Now.AddDays(3)
            };

            _context.Items.Add(entity);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<IEnumerable<TodoItem>> GetIncompleteItemAsync(ApplicationUser user)
        {
            var items = await _context.Items.Where(x => x.IsDone == false && x.OwnerId == user.Id).ToArrayAsync();
            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items.Where(x => x.Id == id && x.OwnerId == user.Id).SingleOrDefaultAsync();//TODO:使用SingleAsync()会发生异常
            if (item == null) return false;

            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
