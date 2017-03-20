using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _383_Phase1_InventoryTracker.Entities
{
    public class InventoryItem
    {
        [Key]
        public int Id { get; set; }


        public int CreatedByUserId { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}
