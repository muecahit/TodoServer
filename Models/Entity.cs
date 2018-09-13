using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoServer.Models
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }

        public Entity()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
