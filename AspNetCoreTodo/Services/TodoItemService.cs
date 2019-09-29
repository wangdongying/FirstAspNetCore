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
        //你应该注意到相同的依赖注入模式，如你在 MVC基础 章节所见到的那样，只是这次被注入的服务是 ApplicationDbContext。ApplicationDbContext 已经在ConfigureServices 方法里被添加到服务容器里，所以在这里可以直接使用。
        public TodoItemService(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddItemAsync(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            todoItem.IsDone = false;
           // todoItem.DueAt = DateTimeOffset.Now.AddDays(10);

            _applicationDbContext.Items.Add(todoItem);

            var saveResult = await _applicationDbContext.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
          var itemlist= await _applicationDbContext.Items
                .Where(x=>x.IsDone==false && x.UserId==user.Id)
                .ToArrayAsync();

            return itemlist;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
         
               var item=await _applicationDbContext.Items.Where(t=>t.Id==id).SingleOrDefaultAsync();
                if (item == null) return false;

                item.IsDone = true;
                var saveResult =await _applicationDbContext.SaveChangesAsync();
            return saveResult==1;
        }

    }
}
