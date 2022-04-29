﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
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
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, UserManager<StoreLibraryUser> userManager)
        {
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
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