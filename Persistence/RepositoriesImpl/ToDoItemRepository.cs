using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoServer.Data;
using ToDoServer.Models;
using ToDoServer.Persistence.RepositoriesI;

namespace ToDoServer.Persistence.RepositoriesImpl
{
    public class ToDoItemRepository : Repository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(ToDoListContext context) : base(context)
        {
        }

        public Task<List<ToDoItem>> GetToDoItemsByListIdAsync(Guid listId)
        {
            return ToDoListContext.ToDoItem
                .Where(item => item.ToDoItemListId == listId)
                .ToListAsync();
        }

        public async Task RenameTodoItem(Guid id, string newText)
        {
            var todoItem = await ToDoListContext.ToDoItem.SingleAsync(item => item.Id == id);
            todoItem.ToDo = newText;
        }

        public void RemoveTodoItemById(Guid id)
        {
            ToDoListContext.ToDoItem.Remove(new ToDoItem { Id = id });
        }

        public ToDoListContext ToDoListContext
        {
            get { return Context as ToDoListContext; }
        }
    }
}
