using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoServer.Models
{
    public class ToDoItemList : Entity
    {
        [Required]
        public string ListName { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
