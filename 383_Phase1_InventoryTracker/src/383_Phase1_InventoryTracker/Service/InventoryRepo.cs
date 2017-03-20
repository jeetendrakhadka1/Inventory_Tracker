using _383_Phase1_InventoryTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http.Description;
namespace _383_Phase1_InventoryTracker.Service
{
    public class InventoryRepo
    {
        _383_Phase1_InventoryTracker.Entities.InventoryTrackerContext db;
        public InventoryRepo()
        {
            this.db = new InventoryTrackerContext();
        }     

        //Get

        public IEnumerable<InventoryItem> GetInventory()
        {
            var InventoryItemsList = db.InventoryItems;
            return InventoryItemsList;
        }

        //Get (ID)
        //[ResponseType(typeof(InventoryItem))]
        public InventoryItem GetInventoryById(int id)
        {
            InventoryItem item = db.InventoryItems.Find(id);

            if (item != null)
            {
                return item;
            }
            return null;
        }

        //POST method

        public void PostInventory(InventoryItem item)
        {
            db.InventoryItems.Add(item);
            db.SaveChanges();

        }

        //Put method
        public void PutInventory(InventoryItem  item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();


        }

        //Delete method
        public bool DeleteInventory(int id)
        {
            InventoryItem item = db.InventoryItems.Find(id);
            if (item != null)
            {
                db.InventoryItems.Remove(item);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool InventoryItemExists(int id)
        {
            return db.InventoryItems.Any(e => e.Id == id);
        }

    }
}
