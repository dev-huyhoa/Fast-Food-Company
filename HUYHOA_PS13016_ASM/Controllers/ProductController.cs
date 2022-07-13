using HUYHOA_PS13016_ASM.Interfaces;
using HUYHOA_PS13016_ASM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HUYHOA_PS13016_ASM.Controllers
{
    public class ProductController : Controller
    {
        private readonly IFoods _foods;
        public ProductController(IFoods foods)
        {
            _foods = foods;
        }
        public ActionResult Index()
        {

            //  ViewBag.products = products.FindAll();
            return View(_foods.GetFoodAll());
        }

        //public async Task<ActionResult> Index()
        //{

        //  //  ViewBag.products = products.FindAll();
        //    return View(await _foods.GetFoodAllAsync());
        //}

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
