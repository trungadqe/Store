#nullable disable
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
using Microsoft.AspNetCore.Authorization;

namespace StoreLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly StoreLibraryContext _context;
        private readonly UserManager<StoreLibraryUser> _userManager;
        private readonly StoreLibraryContext dbcontext;

        public BooksController(StoreLibraryContext context, UserManager<StoreLibraryUser> userManager, StoreLibraryContext dbcontext)
        {
            this.dbcontext = dbcontext;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> UserIndexAsync()
        {
            var Book = _context.Book.Include(b => b.Store);
            return View(await Book.ToListAsync());
        }
        public async Task<IActionResult> CusIndexAsync()
        {
            var Book = _context.Book.Include(b => b.Store);
            return View(await Book.ToListAsync());
        }
        public async Task<IActionResult> UserSearch(string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;
            /*Book book = new Book()
            {
                CategoryList = new List<SelectListItem>
                {
                    new SelectListItem {Value = "Comic", Text = "Comic"},
                    new SelectListItem {Value = "Cartoon", Text = "Cartoon"},
                }
            };*/
            var books = from s in dbcontext.Book
                        .Include(s => s.Store)
                        select s;
            books = books.Where(s => s.Title.Contains(searchString));
            List<Book> booksList = await books.ToListAsync();
            /*ViewData["Category"] = SelectListItem(Category);*/
            return View(books);
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            var storeLibraryContext = _context.Book.Include(b => b.Store);
            return View(await storeLibraryContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Store)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var store = _context.Store
                .Include(s => s.User)
                .FirstOrDefault(x => x.UId == userId);
            if(store == null)
            {
                return RedirectToAction("Create", "Stores", new { area = "" });
            }
            ViewData["CategoryName"] = new SelectList(_context.Category.ToList(), "Name", "Name");
            ViewData["StoreId"] = new SelectList(_context.Store.Where(s => s.Id == store.Id), "Id", "Id");
            /*var id = store.Id;*/
            //ViewData["StoreId"] = new SelectList(_context.Store.Where(c => c.Id == store.Id), "Id", "Id");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Isbn,Title,Pages,Author,Category,Price,Desc,ImgUrl,StoreId")] Book book, IFormFile image)
        {
            if (image != null)
            {
                string imgName = book.Isbn + Path.GetExtension(image.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imgName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                book.ImgUrl = "img/"+ imgName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            var store = _context.Store
                .Include(s => s.User)
                .FirstOrDefault(x => x.UId == userId);
            var id = store.Id;

            ViewData["StoreId"] = new SelectList(_context.Store.Where(c => c.Id == store.Id), "Id", "Id");
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            /*ViewData["CategoryList"] = new SelectList(_context.Book,"Category", "Category", book.CategoryList);*/
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", book.StoreId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Isbn,Title,Pages,Author,Category,Price,Desc,ImgUrl,StoreId")] Book book)
        {
            if (id != book.Isbn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Isbn))
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
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Id", book.StoreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Store)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        
        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Book.Any(e => e.Isbn == id);
        }
    }
}
