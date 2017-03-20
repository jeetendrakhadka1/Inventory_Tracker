using _383_Phase1_InventoryTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Service
{
    interface InventoryInterface
    {
        
        
            IEnumerable<InventoryItem> GetInventory();
            InventoryItem GetInventoryById(int id);
            void PostInventory(InventoryItem inventory);
            void PutInventory(InventoryItem inventory);
        bool DeleteInventory(int id);
        }
    }

