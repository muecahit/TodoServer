using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoServer.Models;

namespace ToDoServer.Persistence.RepositoriesI
{
    public interface IToDoItemListRepository : IRepository<ToDoItemList>
    {
        Task<List<ToDoItemList>> GetToDoItemListsByEmailAsync(string email);
        Task<ToDoItemList> RenameTodoItemList(Guid id, string newListName);
        void RemoveTodoItemListById(Guid id);
    }
}
