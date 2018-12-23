using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CIS_420_WebApplication.Models;

namespace CIS_420_WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
          
        }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<CIS_420_WebApplication.Models.Event> Event { get; set; }
        public DbSet<CIS_420_WebApplication.Models.AuctionItem> AuctionItem { get; set; }
        public DbSet<CIS_420_WebApplication.Models.BoardMember> BoardMember { get; set; }
        public DbSet<CIS_420_WebApplication.Models.Horse> Horse { get; set; }
        public DbSet<CIS_420_WebApplication.Models.DonationPackage> DonationPackage { get; set; }
        public DbSet<CIS_420_WebApplication.Models.Donation> Donation { get; set; }
 

    }
}
