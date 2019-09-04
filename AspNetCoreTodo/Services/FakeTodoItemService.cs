using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<bool> AddItemAsync(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Learn ASP.NET Core",
                DueAt = DateTimeOffset.Now.AddDays(1)
            };

            var item2 = new TodoItem
            {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(2)
            };
            
            return Task.FromResult(new[] { item1, item2 });

        }
    }
    //public class FakeTodoItemService_one : ITodoItemService
    //{
    //    public Task<TodoItem[]> GetIncompleteItemsAsync()
    //    {
    //        var item1 = new TodoItem
    //        {
    //            Title = "111111111111",
    //            DueAt = DateTimeOffset.Now.AddDays(3)
    //        };

    //        var item2 = new TodoItem
    //        {
    //            Title = "222222222222222",
    //            DueAt = DateTimeOffset.Now.AddDays(4)
    //        };

    //        return Task.FromResult(new[] { item1, item2 });

    //    }
    //}


}
