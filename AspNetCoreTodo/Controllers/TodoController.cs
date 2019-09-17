using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        //还记得吗？ GetIncompleteItemsAsync 方法返回一个 Task<TodoItem[]>。“返回一个 Task”的意思是说，该方法不能立刻给出一个结果，但是你可以使用关键字 await，以确保你的代码暂停，直到结果就绪才继续执行。
        //当你编写代码访问数据库或者外部 API 服务的时候，Task 模式是很常见的，因为在数据库（或者网络）响应之前，它不可能给出实际的结果。如果你在 JavaScript 或者其它语言里使用过 promise 或者 回调函数，Task 与之如出一辙：承诺你，肯定会给出一个结果——在未来的某个时候。如果你在老式 JavaScript 里应付过 “回调地狱”，那你现在走运了。在 .NET 里使用 Task 跟依附代码打交道要容易得多，这归功于神奇的关键字 await。 await 把代码暂停在 异步(async) 操作上，而后，在底层数据库或者网络请求结束时，从暂停的地方恢复执行。就是说，你的程序并没有卡住或者阻塞住，因为它可以处理其它的请求。如果现在想不通也别担心，跟着做下去就行！
        public async Task<IActionResult> Index()
        {
            var item = await _todoItemService.GetIncompleteItemsAsync();

            TodoViewModel viewmodel = new TodoViewModel();
            viewmodel.Items = item;

            return View(viewmodel);
        }
        public async Task<TodoItem[]> Index1()
        {
            var item = await _todoItemService.GetIncompleteItemsAsync();

            TodoViewModel viewmodel = new TodoViewModel();

            return item;
        }

        public  async Task<IActionResult> AddItem(TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _todoItemService.AddItemAsync(item);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");

        }


    }
}