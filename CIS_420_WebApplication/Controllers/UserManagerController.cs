using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIS_420_WebApplication.Data;
using CIS_420_WebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CIS_420_WebApplication.Controllers
{
    [Authorize(Roles = "FullAdmin")]
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public UserManagerController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

    
        public async Task<IActionResult> Index()=> View(await _context.AppUsers.ToListAsync());
     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
   
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers.FirstOrDefaultAsync(x=>x.ApplicationUserId==id);
          
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationUserId,FirstName,LastName,AccountCreationDate,Access")] ApplicationUser appUser)
        {
            if (id != appUser.ApplicationUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var EditUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.ApplicationUserId == appUser.ApplicationUserId);
                    _userManager.RemoveFromRoleAsync(EditUser, EditUser.Access.ToString()).Wait();
                    _userManager.AddToRoleAsync(EditUser, appUser.Access.ToString()).Wait();
                                
                    EditUser.Access = appUser.Access;
                    var UpdateTask = _context.Update(EditUser);
                   _context.SaveChanges();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(AppUserExists(id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            if (user == null)
            {
                return NotFound();
            }
          
            return View(user);
        }
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var AppUser =  await _context.AppUsers
                .FirstOrDefaultAsync(m => m.ApplicationUserId == id);
            _context.AppUsers.Remove(AppUser);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(e => e.ApplicationUserId == id);
        }
    }
}