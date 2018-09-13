using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using ToDoServer.Models;

namespace ToDoServer.Data
{
    public class ToDoListContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<ToDoItemList> ToDoItemList { get; set; }

        public ToDoListContext(DbContextOptions<ToDoListContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=todo;Integrated Security=True");
            optionsBuilder.UseInMemoryDatabase("ToDoListContext");
        }

    }
}
