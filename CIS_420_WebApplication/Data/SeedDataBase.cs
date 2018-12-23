using CIS_420_WebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS_420_WebApplication.Data
{
    public class SeedDataBase
    {

        public SeedDataBase(IServiceProvider _serviceProvider, ApplicationDbContext _context)
        {
            serviceProvider = _serviceProvider;
            context = _context;
            
        }
        private IServiceProvider serviceProvider;
        private ApplicationDbContext context;
        private ApplicationUser superUser;

        public async Task Seed()
        {
          
            await CreateSuperUser();
            await SeedDb();
        }
        private async Task CreateSuperUser()
        {
            var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userExists = await _userManager.GetUsersInRoleAsync("FULLADMIN");

            if (userExists.Count() < 1)
            {
                var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var _signinManger = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

                superUser = new ApplicationUser()
                {
                    UserName = "superuser@superuser.com",
                    Email = "superuser@superuser.com",
                    FirstName = "Super",
                    LastName = "User",
                    AccountCreationDate = DateTime.Now.ToShortDateString(),
                    Access = ApplicationUser.Permissions.FullAdmin
                };
                var permissions = Enum.GetNames(typeof(ApplicationUser.Permissions));

                foreach (var s in permissions)
                {
                    await _roleManager.CreateAsync(new IdentityRole(s));
                }
                await _userManager.CreateAsync(superUser, "SecureP@ssword1234");
                await _userManager.AddToRoleAsync(superUser, Enum.GetName(typeof(ApplicationUser.Permissions), superUser.Access));
            }
        }
        private async Task SeedDb()
        {
            
            List<Task> tasks = new List<Task>();

       bool isPopulated1 = context.AuctionItem.Any();           
            if(!isPopulated1)
               tasks.Add(context.AuctionItem.AddAsync(new AuctionItem() { Name = "Default Item", Description="Example Item", Quantity=1, SetPrice=0 }));

            bool isPopulated2 = context.BoardMember.Any();
            if (!isPopulated2)
                tasks.Add(context.BoardMember.AddAsync(new BoardMember() { Name="Default Member",  Description="Example Member", AsList = new List<BoardMember>() } ));

            bool isPopulated3 = context.DonationPackage.Any();
            if (!isPopulated3)
                tasks.Add(context.DonationPackage.AddAsync(new DonationPackage() { PackageName="Default", Amount = 10 }));

            bool isPopulated4 = context.Event.Any();
            if (!isPopulated4)
                tasks.Add(context.Event.AddAsync(new Event() { Name="Default Event", Date=DateTime.Now, Description="Event Example", Link="www.google.com", LinkTitle="Example link", Time=DateTime.Now.ToLocalTime() }));
            bool isPopulated5 = context.Horse.Any();
            if (!isPopulated5)
                tasks.Add(context.Horse.AddAsync(new Horse() {Name="Default Horse", Description="Example Horse" }));

           await Task.WhenAll(tasks);
            context.SaveChanges();
        }
        

    }
}
