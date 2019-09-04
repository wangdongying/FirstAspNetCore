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

        public async Task<TodoItem[]> Index()
        {
            var item = await _todoItemService.GetIncompleteItemsAsync();

            TodoViewModel viewmodel = new TodoViewModel();

            return item;
        }



    }
}