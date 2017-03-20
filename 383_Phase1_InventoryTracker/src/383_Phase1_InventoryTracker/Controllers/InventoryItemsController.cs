using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _383_Phase1_InventoryTracker.Entities;
using Microsoft.AspNetCore.Authorization;

namespace _383_Phase1_InventoryTracker.Controllers
{
    public class InventoryItemsController : Controller
    {
        private readonly InventoryTrackerContext _context;

        public InventoryItemsController(InventoryTrackerContext context)
        {
            _context = context;    
        }

        // GET: InventoryItems
        //[Authorize(Policy ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.InventoryItems.ToListAsync());
        }

        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            return View(await _context.InventoryItems.ToListAsync());
        }


        public async Task<IActionResult> ForUsers()
        {
            return View(await _context.InventoryItems.ToListAsync());
        }

        
        public async Task<IActionResult> Purchase(int id) {

            InventoryItem newItem = new InventoryItem();

          newItem = _context.InventoryItems.FirstOrDefault(s => s.Id.Equals(id));

            newItem.Quantity = newItem.Quantity - 1;
            _context.SaveChanges();
            return View(await _context.InventoryItems.ToListAsync());
        }
        // GET: InventoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: InventoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedByUserId,Name,Quantity")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inventoryItem);
        }

        [Authorize(Policy = "Admin")]
        // GET: InventoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            return View(inventoryItem);
        }

        // POST: InventoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedByUserId,Name,Quantity")] InventoryItem inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemExists(inventoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(inventoryItem);
        }

        [Authorize(Policy = "Admin")]
        // GET: InventoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // POST: InventoryItems/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryItem = await _context.InventoryItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InventoryItemExists(int id)
        {
            return _context.InventoryItems.Any(e => e.Id == id);
        }
    }
}
