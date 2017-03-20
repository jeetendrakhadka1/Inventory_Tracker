using _383_Phase1_InventoryTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Entities
{
    public class InventoryTrackerContext : DbContext
    {
       
        public InventoryTrackerContext(DbContextOptions<InventoryTrackerContext> options) : base(options)
        {

        }

        public InventoryTrackerContext()
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlite("Filename=Inventory.db");
        }

       

    }
}

