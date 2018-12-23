using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CIS_420_WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using CIS_420_WebApplication.Data;
using System.Security.Policy;

namespace CIS_420_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Event> events;
        private readonly List<BoardMember> boardMembers;
        private readonly List<DonationPackage> donationPackages;
        public HomeController(ApplicationDbContext context)
        {
            events = context.Event.ToList();
            boardMembers = context.BoardMember.ToList();
            donationPackages = context.DonationPackage.ToList();
        }
        public IActionResult Index()=>View();
        public IActionResult DBError()=>View();
        public IActionResult About()=>View();
        public IActionResult Contact()=>View();
        public IActionResult Events()=>View(events);
        public IActionResult MeetUs()=>View(boardMembers);
        public IActionResult Donate()=>View(donationPackages);
        public IActionResult Privacy()=>View();
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
