using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoServer.Persistence.RepositoriesI
{
    public interface IUnitOfWork
    {
        IToDoItemRepository ToDoItems { get; }
        IToDoItemListRepository ToDoItemLists { get; }
        int Complete();
    }
}
