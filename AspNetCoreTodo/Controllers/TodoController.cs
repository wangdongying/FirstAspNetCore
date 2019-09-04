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