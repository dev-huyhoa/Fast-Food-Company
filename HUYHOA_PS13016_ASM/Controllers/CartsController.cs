using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUYHOA_PS13016_ASM.Models;
using HUYHOA_PS13016_ASM.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HUYHOA_PS13016_ASM.Controllers
{
    public class CartsController : Controller
    {
        private readonly DataContext _context;
        private IFoods _foodsvc;
        public int count = 0;
        public float sum = 0;
        public CartsController(DataContext context, IFoods food)
        {
            _context = context;
            _foodsvc = food;
        }

        public IActionResult Buy(int id)
        {

            var cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<Items>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }


            Items item = cart.Where(x => x.Foods.FoodID == id).FirstOrDefault();

            if (item == null)
            {
                var product = _context.Foods.Find(id);
                Items newItem = new Items();
                newItem.Foods= product;
                newItem.Quantity = 1;
                newItem.Price = newItem.Quantity * newItem.Foods.FoodPrice;
                cart.Add(newItem);
            }
            else
            {
                item.Quantity++;
                item.Price = item.Price * item.Foods.FoodPrice;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            foreach (var item2 in cart)
            {
                sum += item2.Price;
            }
            ViewBag.count = count;
            return RedirectToAction("Create", "Carts", new { sum});
        }

        private int isExist(int id)
        {
            List<CartDetails> cart = SessionHelper.GetObjectFromJson<List<CartDetails>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Foods.FoodID.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Remove(int id)
        {
            List<CartDetails> cart = SessionHelper.GetObjectFromJson<List<CartDetails>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Create");
        }

        public IActionResult add(int id)
        {
            List<Items> cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");

            int index = isExist(id);
            cart[index].Quantity++;
            cart[index].Price = cart[index].Quantity * cart[index].Foods.FoodPrice;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");

            double sum = 0;
            foreach (var item2 in cart)
            {
                sum += item2.Price;
            }
            return RedirectToAction("Create", new { sum });
        }
        public IActionResult subtract(int id)
        {
            List<Items> cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Quantity--;
            cart[index].Price = cart[index].Quantity * cart[index].Foods.FoodPrice;
            if (cart[index].Quantity == 0)
            {
                return Remove(id);
            }
            double sum = 0;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");

            foreach (var item2 in cart)
            {
                sum += item2.Price;
            }
            return RedirectToAction("Create",new { sum});
        }

        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Carts.Include(c => c.User);
            return View(await dataContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // GET: Carts/Create
        public IActionResult Create(double sum)
        {
            ViewBag.sum = sum;
            var cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                return RedirectToAction("index", "product");
            }
            return View(cart);
        }


        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(float sum)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Items>>(HttpContext.Session, "cart");
            if (ModelState.IsValid)
            {
                float totalprice = 0;
                foreach (var item in cart)
                {
                    totalprice += item.Price;
                }
                string email = HttpContext.Session.GetString("Email");
                int idCus = _context.Users.Where(x => x.Email == email).FirstOrDefault().UserID;
                Carts newCart = new Carts();
                newCart.OrderDate = DateTime.Now;
                newCart.TotalPrice = totalprice;
                newCart.UserID = idCus;
                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
                int idmax = 0;
                if(_context.Carts.ToList().Count() <= 0)
                {
                    idmax = 1;
                }
                else
                {
                    idmax = _context.Carts.Max(x => x.CartId);
                }
                foreach (var item in cart)
                {
                    CartDetails cartdetail = new CartDetails();
                    cartdetail.CartID = idmax;
                    cartdetail.ShipDate = DateTime.Now;
                    cartdetail.Quantity = item.Quantity;
                    cartdetail.FoodID = item.Foods.FoodID;
                    cartdetail.TotalPrice = item.Foods.FoodPrice * item.Quantity;
                    _context.CartDetails.Add(cartdetail);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Product");
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts.FindAsync(id);
            if (carts == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", carts.UserID);
            return View(carts);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartId,Note,TotalPrice,OrderDate,UserID")] Carts carts)
        {
            if (id != carts.CartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartsExists(carts.CartId))
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
            ViewData["UserID"] = new SelectList(_context.Users, "UserID", "Email", carts.UserID);
            return View(carts);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CartId == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carts = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(carts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartsExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    
    }
}
