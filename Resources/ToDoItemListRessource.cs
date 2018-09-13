using System.Collections.Generic;

namespace ToDoServer.Resources
{
    public class ToDoItemListRessource : Ressource
    {
        public string ListName { get; set; }
        public List<ToDoItemRessource> ToDoItems { get; set; }
        public int Amount { get; set; }
    }
}
