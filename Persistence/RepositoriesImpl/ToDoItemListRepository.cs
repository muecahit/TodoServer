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
    public class ToDoItemListRepository : Repository<ToDoItemList>, IToDoItemListRepository
    {
        public ToDoItemListRepository(ToDoListContext context) : base(context)
        {
        }

        public Task<List<ToDoItemList>> GetToDoItemListsByEmailAsync(string email)
        {
            return ToDoListContext.ToDoItemList
                .Where(list => list.User.Email == email)
                .Include(l => l.ToDoItems)
                .ToListAsync();
        }

        public async Task<ToDoItemList> RenameTodoItemList(Guid id, string newListName)
        {
            var todoItemList = await ToDoListContext.ToDoItemList.SingleAsync(list => list.Id == id);
            todoItemList.ListName = newListName;
            return todoItemList;
        }

        public void RemoveTodoItemListById(Guid id)
        {
            ToDoListContext.ToDoItemList.Remove(new ToDoItemList { Id = id });
        }

        public ToDoListContext ToDoListContext
        {
            get { return Context as ToDoListContext; }
        }
    }
}
