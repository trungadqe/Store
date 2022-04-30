using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Areas.Identity.Data;

namespace StoreLibrary.Controllers
{
    public class UserController : Controller
    {
        private readonly StoreLibraryContext _context;
        private readonly UserManager<StoreLibraryUser> _userManager;
        public UserController(StoreLibraryContext context, UserManager<StoreLibraryUser> userManager, StoreLibraryContext dbcontext)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);
           
            if (user == null)
            {
                return NotFound();
            }
            /*ViewData["UId"] = new SelectList(_context.Users.Where(c => c.Id == userId), "Id", "Id");*/
            return View(user);
        }

    }
}
