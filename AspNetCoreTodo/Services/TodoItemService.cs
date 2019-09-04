using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TodoItemService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddItemAsync(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            todoItem.IsDone = false;
            todoItem.DueAt = DateTimeOffset.Now.AddDays(10);

            _applicationDbContext.Items.Add(todoItem);

            var saveResult = await _applicationDbContext.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
          var itemlist= await _applicationDbContext.Items
                .Where(x=>x.IsDone==false)
                .ToArrayAsync();

            return itemlist;
        }
    }
}
