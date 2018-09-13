using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ToDoServer.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<ToDoItemList> ToDoItemLists { get; set; }
    }
}
