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
    public class EventsController : Controller
    {
        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;

        public async Task<IActionResult> Index()
            => View(await _context.Event.ToListAsync());

        public async Task<IActionResult> Details(int? id) 
            => await CRUDHelper<EventsController, Event>.FindDetails(this,_context,id);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId,Name,Date,Time,Description,LinkTitle,Link")] Event hoofEvent) 
            => await CRUDHelper<EventsController, Event>.Creator(this,hoofEvent,_context,ModelState.IsValid,nameof(Index));

        public async Task<IActionResult> Edit(int? id) 
            => await CRUDHelper<EventsController, Event>.EditView(this, _context, id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,Name,Date,Time,Time,Description,LinkTitle,Link")] Event hoofEvent) 
            => await CRUDHelper<EventsController, Event>.Editor(this, hoofEvent, _context, ModelState.IsValid, nameof(Index),id);

        public async Task<IActionResult> Delete(int? id) 
            => await CRUDHelper<EventsController, Event>.DeleteView(this, _context, id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
            => await CRUDHelper<EventsController, Event>.Deletor(this,_context,nameof(Index), id);
    }
}
