using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Areas.Identity.Data;
using StoreLibrary.Models;
using System.Diagnostics;

namespace StoreLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<StoreLibraryUser> _userManager;
        private readonly StoreLibraryContext dbcontext;
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, UserManager<StoreLibraryUser> userManager, StoreLibraryContext dbcontext)
        {
            this.dbcontext = dbcontext;
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
        }

       
        public IActionResult Index()
        {
            return RedirectToAction("UserIndex", "Books", new { area = "" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}