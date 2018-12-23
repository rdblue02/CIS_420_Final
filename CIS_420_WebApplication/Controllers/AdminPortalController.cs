using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIS_420_WebApplication.Controllers
{
         [Authorize(Roles ="FullAdmin,PartialAdmin")]
    public class AdminPortalController : Controller
    {
        public ActionResult Portal() => View();
    }
}