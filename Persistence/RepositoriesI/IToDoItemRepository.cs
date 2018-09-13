using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoServer.Models;

namespace ToDoServer.Persistence.RepositoriesI
{
    public interface IToDoItemRepository : IRepository<ToDoItem>
    {
        Task<List<ToDoItem>> GetToDoItemsByListIdAsync(Guid listId);
        void RemoveTodoItemById(Guid id);
        Task RenameTodoItem(Guid id, string newText);
    }
}
