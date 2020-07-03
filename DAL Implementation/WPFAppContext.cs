using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Implementation
{
    public class WPFAppContext : DbContext
    {
        public WPFAppContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Employees");
        }

        public DbSet<Employee> Employees { get; set; }
        
        
    }
}
