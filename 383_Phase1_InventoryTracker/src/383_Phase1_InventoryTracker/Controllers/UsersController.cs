using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _383_Phase1_InventoryTracker.Entities;
using CryptoHelper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _383_Phase1_InventoryTracker.Controllers
{
    public class UsersController : Controller
    {
        private readonly InventoryTrackerContext _context;

        public UsersController(InventoryTrackerContext context)
        {
            _context = context;
        }


        // GET: Users
        [Authorize(Policy="Admin")]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Users.ToListAsync());
        }

        [Authorize(Policy = "Admin")]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        [Authorize(Policy = "Admin")]
        public ActionResult AddUser()
        {
            return View();

        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Crypto.HashPassword(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //[Authorize(Policy = "Admin")]
        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Password,UserName")] User user)
        {


            if (!ModelState.IsValid)
            {
                return View("Create", user);
            }
            User DatabaseObject = new User();

            if (ModelState.IsValid)
            {
                //Searching if the username already exists in the database. 
                User ObjectFromDatabase = _context.Users.FirstOrDefault(s => s.UserName.Equals(user.UserName));
                if (ObjectFromDatabase != null)
                {
                    ModelState.AddModelError("Error", "Username already exists. Please choose another username.");
                    return View(user);
                }
                if (ObjectFromDatabase == null)
                {

                    //Creating an object to save in the database
                    DatabaseObject.FirstName = user.FirstName;
                    DatabaseObject.LastName = user.LastName;
                    //Hashing Password
                    DatabaseObject.Password = Crypto.HashPassword(user.Password);

                    DatabaseObject.UserName = user.UserName;
                    DatabaseObject.Role = "User";

                    _context.Add(DatabaseObject);
                    _context.SaveChanges();
                    var claims = new List<Claim>
                        {
                            new Claim("HasAccess", "True"),
                            new Claim("Username", user.UserName),
                            new Claim ("Role","User")
                        };

                    var claimsIdentity = new ClaimsIdentity(claims, "password");
                    var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", claimsPrinciple);

                    return RedirectToAction("Index", "Home");
                }

                //ViewBag.Message("The username already exists");               

            }
            return RedirectToAction("Create");
        }

        public ActionResult SignIn()
        {
            return View();
        }


        //Post: Siging In
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind("UserName,Password")] User user)
        {
            if (user != null)
            {
                //Check if the User is registered. 
                User CheckUser = _context.Users.FirstOrDefault(s => s.UserName.Equals(user.UserName));
                try
                {
                    bool a = Crypto.VerifyHashedPassword(CheckUser.Password, user.Password);
                    if (a == true)
                    {

                        //return RedirectToAction("Index", "Home");
                        // creating authetication ticket                        
                        // creating authetication ticket  
                        var claims = new List<Claim>
                        {
                            new Claim("HasAccess", "True"),
                            new Claim("Username", CheckUser.UserName),
                            new Claim("Role",CheckUser.Role)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, "password");
                        var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                        HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", claimsPrinciple);

                        if (CheckUser.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Users"); ;
                        }
                        else
                        {

                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Invalid Username/Password combination. Please try again");
                        return View(user);
                    }
                }
                catch
                {
                    ModelState.AddModelError("Error", "Invalid Username/Password combination. Please try again");
                    return View(user);
                }


            }


            else
            {
                return View(user);
            }
        }




        public ActionResult SignOut()
        {
            HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Policy = "Admin")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Password,UserName")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        [Authorize(Policy = "Admin")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize(Policy = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //Adding admin to the database.
        public void AddAdmin()
        {
            User admin = new User();
            var password = Crypto.HashPassword("selu2017");
            admin.Role = "Admin";
            admin.UserName = "admin";
            admin.FirstName = "Admin";
            admin.LastName = "Admin";
            _context.Add(admin);
            _context.SaveChanges();



        }

    }


}

