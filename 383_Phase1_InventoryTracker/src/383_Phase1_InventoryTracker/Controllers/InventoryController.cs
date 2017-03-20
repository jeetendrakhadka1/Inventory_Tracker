using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _383_Phase1_InventoryTracker.Entities;
using _383_Phase1_InventoryTracker.Service;
using _383_Phase1_InventoryTracker.Entities.DTO;
using Microsoft.AspNetCore.Authorization;

namespace _383_Phase1_InventoryTracker.Controllers
{
    [Produces("application/json")]
   // [Route("api/Inventory")]
    public class InventoryController : Controller
    {
        private readonly InventoryTrackerContext _context;
        InventoryRepo repo = new InventoryRepo();
        DTOFactory _factory = new DTOFactory();

        public InventoryController(InventoryTrackerContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        //[Route("GetInventory")]
        public IEnumerable<InventoryItem> GetInventoryItems()
        {
          return _context.InventoryItems;
          
        }
              
        [HttpGet]
      //  [Route("GetInventoryById")]
        public IActionResult GetInventoryItem(int id)
        {
            InventoryItem item = _context.InventoryItems.Find(id);
         
            if (item != null)
            {              
                return Ok(item);
            }

            return BadRequest();
        }

        // PUT: api/InventoryApi/5
        [HttpPut("{id}")]
        public IActionResult PutInventoryItem(int id, InventoryItem item)
        {
            if (ModelState.IsValid && (id == item.Id))
            {
                repo.PutInventory(item);
                var returnItems = _factory.CreateInventoryDTO(item);
                return CreatedAtRoute("DefaultApi", new { id = item.Id }, returnItems);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST: api/InventoryApi
        [HttpPost]
        public IActionResult PostInventoryItem(InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                repo.PostInventory(item);

            }

            var returnables = _factory.CreateInventoryDTO(item);

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, returnables);
        }

        // DELETE: api/InventoryApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInventoryItem(int id)
        {
            //Footballers footballers;
            bool value = repo.DeleteInventory(id);
            if (value)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }    
   
    }
}