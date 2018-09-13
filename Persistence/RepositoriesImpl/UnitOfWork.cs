using ToDoServer.Data;
using ToDoServer.Persistence.RepositoriesI;

namespace ToDoServer.Persistence.RepositoriesImpl
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListContext context;

        public IToDoItemRepository ToDoItems { get; private set; }
        public IToDoItemListRepository ToDoItemLists { get; private set; }

        public UnitOfWork(ToDoListContext context)
        {
            this.context = context;
            ToDoItems = new ToDoItemRepository(context);
            ToDoItemLists = new ToDoItemListRepository(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
