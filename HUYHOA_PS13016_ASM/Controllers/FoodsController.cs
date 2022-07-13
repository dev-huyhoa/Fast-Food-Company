using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUYHOA_PS13016_ASM.Models;
using Microsoft.AspNetCore.Http;

namespace HUYHOA_PS13016_ASM.Controllers
{
    public class FoodsController : Controller
    {
        private readonly DataContext _context;

        public FoodsController(DataContext context)
        {
            _context = context;
        }


        // GET: Foods
        public async Task<IActionResult> Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            var dataContext = _context.Foods.Include(f => f.Category);
            return View(await dataContext.ToListAsync());
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foods = await _context.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (foods == null)
            {
                return NotFound();
            }
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View(foods);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            ViewData["CategoryID"] = new SelectList(_context.CategoryModels, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodID,FoodName,CategoryID,FoodAmout,FoodPrice,FoodImage,CreateDate,IsDelete")] Foods foods)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foods);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.CategoryModels, "CategoryID", "CategoryID", foods.CategoryID);
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View(foods);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foods = await _context.Foods.FindAsync(id);
            if (foods == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.CategoryModels, "CategoryID", "CategoryID", foods.CategoryID);
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View(foods);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodID,FoodName,CategoryID,FoodAmout,FoodPrice,FoodImage,CreateDate,IsDelete")] Foods foods)
        {
            if (id != foods.FoodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foods);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodsExists(foods.FoodID))
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
            ViewData["CategoryID"] = new SelectList(_context.CategoryModels, "CategoryID", "CategoryID", foods.CategoryID);
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View(foods);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foods = await _context.Foods
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.FoodID == id);
            if (foods == null)
            {
                return NotFound();
            }
            ViewBag.UserName = HttpContext.Session.GetString("UserFullName");
            return View(foods);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foods = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(foods);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodsExists(int id)
        {
            return _context.Foods.Any(e => e.FoodID == id);
        }
    }
}
