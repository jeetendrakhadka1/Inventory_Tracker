using CryptoHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Entities
{
    //Seeding an admin to the database.
    public static class DbInitializer
    {
        public static void Initialize(InventoryTrackerContext context)
        {
            context.Database.EnsureCreated();

            //Look for any users.
           if (context.Users.Any())
           {
               return;   // DB has been seeded
           }
            

            var user = new User[]
            {
            new User{FirstName="Administrator",LastName="Maestro",Role="Admin",UserName="admin",Password=Crypto.HashPassword("selu2017")},
            new User{FirstName="Willy",LastName="Wonka",Role="User",UserName="sociopathcandyman",Password=Crypto.HashPassword("childendangerment")},
            new User{FirstName="Ted",LastName="Cruz",Role="Admin",UserName="itwasnotme",Password=Crypto.HashPassword("zodiac")},
            };
            foreach (User u in user)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var items = new InventoryItem[]
            {
            new InventoryItem{Name= "Retro Encabulator",Quantity= 85,CreatedByUserId= 0},
            new InventoryItem{Name= "Plumbus",Quantity= 150,CreatedByUserId= 0},
            new InventoryItem{Name= "Quantum Battery",Quantity= 200,CreatedByUserId= 0}
            };

            foreach (InventoryItem item in items)
            {
                context.InventoryItems.Add(item);
            }
            context.SaveChanges();
        }
    }
}
