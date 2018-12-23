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
    public class DonationPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index() => View(await _context.DonationPackage.ToListAsync());
        public async Task<IActionResult> Details(int? id) => await CRUDHelper<DonationPackagesController, DonationPackage>.FindDetails(this, _context, id);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UId, PackageName, Amount, Description, ImgUrl")] DonationPackage donationPackage)
        => await CRUDHelper<DonationPackagesController, DonationPackage>.Creator(this, donationPackage, _context, ModelState.IsValid, nameof(Index));

        public async Task<IActionResult> Edit(int? id)
        => await CRUDHelper<DonationPackagesController, DonationPackage>.EditView(this, _context, id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UId,PackageName,Amount,Description,ImgUrl")]DonationPackage donationPackage)
        => await CRUDHelper<DonationPackagesController, DonationPackage>.Editor(this, donationPackage, _context, ModelState.IsValid, nameof(Index), id);
        public async Task<IActionResult> Delete(int? id)
        => await CRUDHelper<DonationPackagesController, DonationPackage>.DeleteView(this, _context, id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        => await CRUDHelper<DonationPackagesController, DonationPackage>.Deletor(this, _context, nameof(Index), id);
       
    }
}
