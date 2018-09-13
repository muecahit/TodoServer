using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoServer.Resources
{
    public class ToDoItemRessource : Ressource
    {
        public string ToDo { get; set; }

        public string CreationDate { get; set; }

        public Guid ToDoItemListId { get; set; }
    }
}
