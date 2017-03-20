using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Entities.DTO
{
    public class DTOFactory
    {
        public InventoryDTO CreateInventoryDTO(InventoryItem items)

        {
            InventoryDTO _inventoryDTO = new InventoryDTO
            {

                Name = items.Name,
                Quantity = items.Quantity,
                           };




            return _inventoryDTO;
        }
    }

}

