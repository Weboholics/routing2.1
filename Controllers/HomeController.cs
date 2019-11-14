using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Routing_20.Models;

namespace Routing_20.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string culture)
        {
            ActionResult result;
            if (culture == null)  // If no given culture - redirect to culture specific page
            {
                result = new RedirectResult(Url.Action("Index", "Home", new { culture = "sv-se" }));
            }
            else
            {
                result = View();
            }
            return result;
        }
    }
}
