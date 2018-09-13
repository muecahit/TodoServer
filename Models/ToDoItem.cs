using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoServer.Models
{
    public class ToDoItem : Entity
    {
        public ToDoItem() : base()
        {
            this.CreationDate = DateTime.Now;
        }
        
        [Required]
        public string ToDo { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public Guid ToDoItemListId { get; set; }

        [ForeignKey("ToDoItemListId")]
        public virtual ToDoItemList ToDoItemList { get; set; }
    }
}