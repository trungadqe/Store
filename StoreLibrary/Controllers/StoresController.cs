﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeWEB.Models;
using StoreLibrary.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace StoreLibrary.Controllers
{
    public class StoresController : Controller
    {
        private readonly StoreLibraryContext _context;
        private readonly UserManager<StoreLibraryUser> _userManager;
        public StoresController(StoreLibraryContext context, UserManager<StoreLibraryUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
        {
            var userName = await _userManager.GetUserAsync(HttpContext.User);
            var rolesname = await _userManager.GetRolesAsync(userName);
            var userId = _userManager.GetUserId(HttpContext.User);
            var store = _context.Store.FirstOrDefault(x => x.UId == userId);
            var storeLibraryContext = _context.Store.Include(s => s.User);
            if (rolesname.Contains("Seller"))
            {
                if (store == null)
                {
                    return RedirectToAction("Create", "Stores", new { area = "" });
                }
                return RedirectToAction("Details", "Stores", new { area = "" });
            }
            return View(await storeLibraryContext.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var store = _context.Store
            .Include(s => s.User)
            .FirstOrDefault(x => x.UId == userId);
            var id = store.Id;
            if (id == null)
            {
                return NotFound();
            }
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewData["UId"] = new SelectList(_context.Users.Where(c => c.Id == userId), "Id", "Id");
            /*ViewData["UId"] = new SelectList(_context.Users, "Id", "Id");*/
            return View();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Slogan,UId")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewData["Uid"] = new SelectList(_context.Users.Where(c => c.Id == userId), "Id", "Id");
            /*ViewData["UId"] = new SelectList(_context.Users, "Id", "Id", store.UId);*/
            return RedirectToAction("Index", "Stores", new { area = "" });
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var store = _context.Store
                .Include(s => s.User)
                .FirstOrDefault(x => x.UId == userId);
            var id = store.Id;
            if (id == null)
            {
                return NotFound();
            }
            if (store == null)
            {
                return NotFound();
            }
            ViewData["UId"] = new SelectList(_context.Users.Where(c => c.Id == userId), "Id", "Id");
            return View(store);
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Slogan,UId")] Store store)
        {
            if (id != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewData["UId"] = new SelectList(_context.Users.Where(c => c.Id == userId), "Id", "Id");
            return View(store);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store = await _context.Store
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Store.FindAsync(id);
            _context.Store.Remove(store);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Store.Any(e => e.Id == id);
        }
    }
}
