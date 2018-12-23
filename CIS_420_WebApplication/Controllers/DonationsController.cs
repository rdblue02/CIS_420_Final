using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIS_420_WebApplication.Data;
using CIS_420_WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using CIS_420_WebApplication.Controllers;
namespace CIS_420_WebApplication
{
    [Authorize(Roles = "FullAdmin,PartialAdmin")]
    public class DonationsController : Controller
    {
        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public async Task<IActionResult> Index()
            => View(await _context.Donation.ToListAsync());

        public async Task<IActionResult> Details(int? id) 
            => await CRUDHelper<DonationsController, Donation>.FindDetails(this,_context,id);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId,Date,Amount")] Donation donation)
            => await CRUDHelper<DonationsController, Donation>.Creator(this,donation,_context,ModelState.IsValid,nameof(Index));

        public async Task<IActionResult> Edit(int? id) 
            => await CRUDHelper<DonationsController, Donation>.EditView(this, _context, id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,Date,Amount")] Donation donation) 
            => await CRUDHelper<DonationsController, Donation>.Editor(this,donation,_context,ModelState.IsValid,nameof(Index),id);

        public async Task<IActionResult> Delete(int? id)
            => await CRUDHelper<DonationsController, Donation>.DeleteView(this, _context, id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
            => await CRUDHelper<DonationsController, Donation>.Deletor(this,_context,nameof(Index),id);
    
    }
}
