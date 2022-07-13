using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUYHOA_PS13016_ASM.Models;

namespace HUYHOA_PS13016_ASM.Controllers
{
    public class RegisterController : Controller
    {
        private readonly DataContext _context;
        private string _message = "";
        public RegisterController(DataContext context)
        {
            _context = context;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Users.Include(u => u.Roles);
            return View(await dataContext.ToListAsync());
        }

        // GET: Register/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserFullName,Email,UserPassWord,Gender,UserBirthDay,UsersPhone,UserAddress,RoleID,IsDelete")] Users users)
        {
            if (EmailExits(users.Email))
            {
                ViewBag.message = "Email is exits";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Add(users);
                    users.RoleID = 2;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Login");

                }
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", users.RoleID);
            return View(users);
        }

        // GET: Register/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", users.RoleID);
            return View(users);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserFullName,Email,UserPassWord,Gender,UserBirthDay,UsersPhone,UserAddress,RoleID,IsDelete")] Users users)
        {
            if (id != users.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserID))
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
            ViewData["RoleID"] = new SelectList(_context.Roles, "RoleID", "RoleName", users.RoleID);
            return View(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
        private bool EmailExits(string email)
        {
            return _context.Users.Any(e =>e.Email == email);
        }
    }
}
