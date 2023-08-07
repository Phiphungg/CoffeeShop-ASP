using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        CoffeeDBDataContext _context = new CoffeeDBDataContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListCoffee()
        {
            var coffees = _context.CoffeeShops.ToList();
            return Json(coffees, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] CoffeeShop coffee)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(coffee.ImagePath);

                coffee.ImagePath = "images/" + fileName;
                _context.CoffeeShops.InsertOnSubmit(coffee);
                _context.SubmitChanges();
            }
            return Json(coffee, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var movie = _context.CoffeeShops.ToList().Find(m => m.Id == id);
            if (movie != null)
            {
                _context.CoffeeShops.DeleteOnSubmit(movie);
                _context.SubmitChanges();
            }
            return Json(movie, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int id)
        {
            var movie = _context.CoffeeShops.ToList().Find(m => m.Id == id);

            string rootPath = Server.MapPath("~/");
            string fileName = System.IO.Path.GetFileName(movie.ImagePath);
            string destFile = System.IO.Path.Combine(rootPath, "Assets\\images\\" + fileName);
            movie.ImagePath = destFile;

            return Json(movie, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(CoffeeShop movie)
        {
            if (ModelState.IsValid)
            {
                string rootPath = Server.MapPath("~/");
                string fileName = System.IO.Path.GetFileName(movie.ImagePath);
                movie.ImagePath = "images/" + fileName;

                CoffeeShop mv = (from p in _context.CoffeeShops
                                 where p.Id == movie.Id
                                  select p).SingleOrDefault();

                mv.CoffeeName = movie.CoffeeName;
                mv.ImagePath = movie.ImagePath;
                mv.Type = movie.Type;
                _context.SubmitChanges();

            }
            return Json(movie, JsonRequestBehavior.AllowGet);
        }
    }
}
