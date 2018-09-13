using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoServer.Resources;
using AutoMapper;
using ToDoServer.Persistence.RepositoriesI;
using Microsoft.AspNetCore.Identity;
using ToDoServer.Models;

namespace ToDoServer.Controllers
{
    [Authorize]
    [Route("api")]
    public class ToDoController : Controller
    {
        public IMapper Mapper { get; }
        public IUnitOfWork Uof { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        public ToDoController(IMapper mapper, IUnitOfWork uof, UserManager<ApplicationUser> userManager)
        {
            Mapper = mapper;
            Uof = uof;
            UserManager = userManager;
        }

        [HttpGet("todoItemLists/getAll")]
        public async Task<IActionResult> GetTodoItemLists()
        {
            var user = await CurrentUserAsync();
            var lists = await Uof.ToDoItemLists.GetToDoItemListsByEmailAsync(user.Email);
            var mappedlists = Mapper.Map<List<ToDoItemList>, List<ToDoItemListRessource>>(lists);

            return Ok(mappedlists);
        }

        [HttpGet("todoItemLists/get")]
        public IActionResult GetTodoItemList(Guid todoItemListId)
        {
            var todoItemList = Uof.ToDoItemLists.Get(todoItemListId);
            var mappedTodoItemList = Mapper.Map<ToDoItemList, ToDoItemListRessource>(todoItemList);

            return Ok(mappedTodoItemList);
        }

        [HttpPost("todoItemLists/create")]
        public async Task<IActionResult> CreateTodoItemList(string listName)
        {
            var newTodoItemList = new ToDoItemList { ListName = listName, UserId = (await CurrentUserAsync()).Id };

            Uof.ToDoItemLists.Add(newTodoItemList);
            Uof.Complete();

            return Ok(Mapper.Map<ToDoItemList, ToDoItemListRessource>(newTodoItemList));
        }

        [HttpGet("todoItems/get")]
        public async Task<IActionResult> GetTodoItems(Guid listId)
        {
            var todos = await Uof.ToDoItems.GetToDoItemsByListIdAsync(listId);

            return Ok(Mapper.Map<List<ToDoItem>, List<ToDoItemRessource>>(todos));
        }

        [HttpGet("todoItems/{todoItemId}")]
        public IActionResult GetTodoItem(Guid todoItemId)
        {
            var todoItem = Uof.ToDoItems.Get(todoItemId);
            var mappedTodoItem = Mapper.Map<ToDoItem, ToDoItemRessource>(todoItem);

            return Ok(mappedTodoItem);
        }

        [HttpPut("todoItems/rename")]
        public async Task<IActionResult> RenameTodoItem(Guid id, string newText)
        {
            await Uof.ToDoItems.RenameTodoItem(id, newText);
            Uof.Complete();

            return Ok();
        }

        [HttpPost("todoItems/create")]
        public IActionResult CreateTodoItem(string todo, Guid todoItemListId)
        {
            //ToDoItem[] todos = { };
            //for (int i = 0; i < 50; i++)
            //{
            //    Uof.ToDoItems.Add(new ToDoItem { ToDo = todo + i, ToDoItemListId = todoItemListId });
            //}
            var newToDoItem = new ToDoItem { ToDo = todo, ToDoItemListId = todoItemListId };

            Uof.ToDoItems.Add(newToDoItem);
            //Uof.ToDoItems.AddRange(todos);
            Uof.Complete();

            return Ok(Mapper.Map<ToDoItem, ToDoItemRessource>(newToDoItem));
            //return Ok();
        }

        [HttpPut("todoItemLists/rename")]
        public async Task<IActionResult> RenameTodoItemList(Guid id, string newListName)
        {
            var newTodoItemList = await Uof.ToDoItemLists.RenameTodoItemList(id, newListName);

            Uof.Complete();

            return Ok(Mapper.Map<ToDoItemList, ToDoItemListRessource>(newTodoItemList));
        }

        [HttpDelete("todoItemLists/remove")]
        public IActionResult RemoveTodoItemList(Guid id)
        {
            Uof.ToDoItemLists.RemoveTodoItemListById(id);
            Uof.Complete();

            return NoContent();
        }

        [HttpDelete("todoItems/remove")]
        public IActionResult RemoveTodoItem(Guid id)
        {
            Uof.ToDoItems.RemoveTodoItemById(id);
            Uof.Complete();

            return NoContent();
        }

        public Task<ApplicationUser> CurrentUserAsync()
        {
            return UserManager.FindByNameAsync(User.Identity.Name);
        }
    }
}
