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
namespace CIS_420_WebApplication.Controllers
{
    [Authorize(Roles = "FullAdmin,PartialAdmin")]
    public class AuctionItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuctionItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.AuctionItem.ToListAsync());
        public async Task<IActionResult> Details(int? id) => await CRUDHelper<AuctionItemsController, AuctionItem>.FindDetails(this,_context,id);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId,Name,SetPrice,Quantity,Description")] AuctionItem auctionItem)
        => await CRUDHelper<AuctionItemsController, AuctionItem>.Creator(this,auctionItem,_context,ModelState.IsValid,nameof(Index));

        public async Task<IActionResult> Edit(int? id)
        => await CRUDHelper<AuctionItemsController, AuctionItem>.EditView(this,_context,id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,Name,SetPrice,Quantity,Description")] AuctionItem auctionItem)
        => await CRUDHelper<AuctionItemsController, AuctionItem>.Editor(this,auctionItem,_context,ModelState.IsValid,nameof(Index),id);
        public async Task<IActionResult> Delete(int? id)
        => await CRUDHelper<AuctionItemsController, AuctionItem>.DeleteView(this,_context,id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        => await CRUDHelper<AuctionItemsController, AuctionItem>.Deletor(this,_context,nameof(Index), id);
    }
}
